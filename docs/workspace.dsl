workspace {
    !identifiers hierarchical

    name "Solution Architecture Diagram"
    description "Draw the architectural diagram of the solution as if you were designing it from scratch."

    model {
        properties {
            "structurizr.groupSeparator" "/"
        }
    
        WarehouseUser = person "Warehouse User" "" "#External"
        Manager = person "Manager" "" "#External, #WebBrowser"
        OnlineUser = person "Online User" "" "#External,#Device"
        Courier = person "Courier User" "" "#External,#Device"

        systemLegacy = softwareSystem "1. Centralized" {
            Database = container "Central Database" "There is an MS SQL DATABASE on the server." "" "#Database"
            
            CourierMobileApp = container "Courier Mobile Application" "Mobile version for couriers" "" "#Device"
            CustomerMobileApp = container "Customer Mobile Application" "Mobile version for customers (buyers)" "#Device"
            
            WarehouseApplication = container "Warehouse - Storage & Delivery" "Desktop application for the warehouse" {
                DesktopUI = component "Desktop UI"
                Authentication = component "Authentication"
                BusinessLogicLayer = component "BLL"
                DataAccessLayer = component "DAL"
            }
            BackOfficeApplication = container "Back Office - Orders Processing" "Web version for managers" {
                WebUI = component "Web UI" "ASP.NET MVC"
                Authentication = component "Authentication"
                BusinessLogicLayer = component "BLL"
                DataAccessLayer = component "DAL"
            }
            OnlineShopApplication = container "Online Shop - Orders Placement" "Web version for customers (buyers)" {
                WebUI = component "Web UI" "ASP.NET MVC"
                Authentication = component "Authentication"
                BusinessLogicLayer = component "BLL"
                DataAccessLayer = component "DAL"
            }
            CustomerService = container "Mobile Service" "Mobile App Service" "" "#ApiService" {
                WebApi = component "Web API" "ASP.NET MVC"
                Authentication = component "Authentication"
                BusinessLogicLayer = component "BLL"
                DataAccessLayer = component "DAL"
            }
            
            # Mobile Application interactions
            CourierMobileApp -> CustomerService "Uses"
            CustomerMobileApp -> CustomerService "Uses"
            
            # Database interactions
            WarehouseApplication -> Database "Queries"
            BackOfficeApplication -> Database "Queries"
            OnlineShopApplication -> Database "Queries"
            CustomerService -> Database "Queries"
            
            # User interactions
            WarehouseUser -> WarehouseApplication "Uses"
            Manager -> BackOfficeApplication "Uses"
            OnlineUser -> OnlineShopApplication "Uses"
            OnlineUser -> CustomerMobileApp "Uses"
            Courier -> CourierMobileApp "Uses"
        }
        systemTireArchitecture = softwareSystem "2. Multi-Tire" {
            Database = container "Central Database" "There is an MS SQL DATABASE on the server." "" "#Database"
            
            CourierMobileApp = container "Courier Mobile Application" "Mobile version for couriers" "" "#Device"
            CustomerMobileApp = container "Customer Mobile Application" "Mobile version for customers (buyers)" "" "#Device"
            
            WarehouseApplication = container "Warehouse - Storage & Delivery" "Desktop application for the warehouse" {
                DesktopUI = component "Desktop UI"
                InfrastructureLayer = component "Service Communication"

                DesktopUI -> InfrastructureLayer
            }
            BackOfficeApplication = container "Back Office - Orders Processing" "Web version for managers" {
                WebUI = component "Web UI" "ASP.NET MVC"
                InfrastructureLayer = component "Service Communication"
             
                WebUI -> InfrastructureLayer
            }
            OnlineShopApplication = container "Online Shop - Orders Placement" "Web version for customers (buyers)" {
                WebUI = component "Web UI" "ASP.NET MVC"
                InfrastructureLayer = component "Service Communication"
             
                WebUI -> InfrastructureLayer
            }
            CustomerService = container "Mobile Service" "Mobile App Service" "" "#ApiService" {
                WebApi = component "Web API" "ASP.NET MVC"
                InfrastructureLayer = component "Service Communication"
             
                WebApi -> InfrastructureLayer
            }
            group "Common Tires" {
                Authentication = container "Authentication"
                BusinessLogicLayer = container "Business Logic Tire" {
                    this -> Authentication "Validates Access"

                    WarehouseApplication -> this "Uses"
                    BackOfficeApplication -> this "Uses"
                    OnlineShopApplication -> this "Uses"
                    CustomerService -> this "Uses"
                }
                DataAccessLayer = container "Data Access Tire" {
                    BusinessLogicLayer -> this "Queries"
                    this -> Database "Uses"
                }
            }

            # Mobile Application interactions
            CourierMobileApp -> CustomerService "Uses"
            CustomerMobileApp -> CustomerService "Uses"
            
            # User interactions
            WarehouseUser -> WarehouseApplication "Uses"
            Manager -> BackOfficeApplication "Uses"
            OnlineUser -> OnlineShopApplication "Uses"
            OnlineUser -> CustomerMobileApp "Uses"
            Courier -> CourierMobileApp "Uses"
        }
        systemDistributed = softwareSystem "3. Distributed" {
            Authentication = container "Authentication"
            OrdersDatabase = container "Orders Database" "There is an MS SQL DATABASE on the server." "" "#Database"
            ReportDatabase = container "Reporting Database" "Limited Access for 3rd Party Provider" "" "#Database"
            SynchronizationBroker = container "Synchronization Broker/Queue" "" "" "#Queue" {
                this -> OrdersDatabase "Writes to"
                this -> ReportDatabase "Writes to"
            }
            group "Authentication on 3rd Party Service" {
                CourierMobileApp = container "Courier Mobile Application" "Mobile version for couriers"
                CustomerMobileApp = container "Customer Mobile Application" "Mobile version for customers (buyers)"
                WarehouseApplication = container "Warehouse - Storage & Delivery" "Desktop application for the warehouse" "" "#Device" {
                    DesktopUI = component "Desktop UI"
                    InfrastructureLayer = component "Service Communication"
                    DesktopUI -> InfrastructureLayer
                }
                BackOfficeApplication = container "Back Office - Orders Processing" "Web version for managers" {
                    WebUI = component "Web UI" "ASP.NET MVC"
                    InfrastructureLayer = component "Service Communication"
                    WebUI -> InfrastructureLayer
                }
                OnlineShopApplication = container "Online Shop - Orders Placement" "Web version for customers (buyers)" {
                    WebUI = component "Web UI" "ASP.NET MVC"
                    InfrastructureLayer = component "Service Communication"
                    WebUI -> InfrastructureLayer
                }
            }
            CustomerService = container "Customer Service" "Incorporates mobile features" "" "#ApiService" {
                WebApi = component "Web API" "ASP.NET MVC"
                InfrastructureLayer = component "Service Communication"
            
                WebApi -> InfrastructureLayer

                # Application interactions
                CourierMobileApp -> this "Uses"
                CustomerMobileApp -> this "Uses"
                OnlineShopApplication -> this "Uses"
            }
            ManagementService = container "Management Service" "Incorporates management use cases" "" "#ApiService" {
                WarehouseApplication.InfrastructureLayer -> this "Process order"
                BackOfficeApplication.InfrastructureLayer -> this "Process order"
            }
            group "Business Logic - Commands" {
                DeliveryWriteService = container "Delivery Write Service" "" "" "#ApiService" {
                    WebApi = component "Web API"

                    ManagementService -> this "Updates delivery info"
                    this -> SynchronizationBroker "Updates state"
                }
                OrderWriteService = container "Ordering Write Service" "" "" "#ApiService" {
                    WebApi = component "Web API"

                    ManagementService -> this "Processes orders"
                    CustomerService -> this "Uses"
                    this -> SynchronizationBroker "Updates state"
                }
            }
            group "Data Access - Queries" {
                OrderReadService = container "Order Read Service" "" "" "#ApiService"{
                    WebApi = component "Web API"

                    ManagementService -> this "Queries"
                    this -> OrdersDatabase "Reads"
                }
                DeliveryReadService = container "Delivery Read Service" "" "" "#ApiService"{
                    WebApi = component "Web API"

                    ManagementService -> this "Queries"
                    CustomerService -> this "Queries"
                    this -> OrdersDatabase "Reads"
                }
            }
            # User interactions
            WarehouseUser -> WarehouseApplication "Uses"
            Manager -> BackOfficeApplication "Uses"
            OnlineUser -> OnlineShopApplication "Uses"
            OnlineUser -> CustomerMobileApp "Uses"
            Courier -> CourierMobileApp "Uses"
        }
        ThirdPartyAuthentication = softwareSystem "3rd Party Authentication" "" "#ApiService,#External,#Auth" {
            systemTireArchitecture.Authentication -> this "Authorizes/Authenticates with"
            systemDistributed.Authentication -> this "Authorizes/Authenticates with"
        }
        ReportPortal = softwareSystem "3rd Party Report Service" "" "#External" {
            this -> systemLegacy.Database "Reads"
            this -> systemTireArchitecture.Database "Reads"
            this -> systemDistributed.ReportDatabase "Reads"
        }
    }
    views {
        container systemLegacy {
            include *
            autoLayout tb
        }
        container systemTireArchitecture {
            include *
            autoLayout tb
        }
        container systemDistributed {
            include *
            autoLayout tb
        }
        styles {
            theme default
            
            element "#External" {
                background #999999
                color #ffffff
            }
            element "#Queue" {
                shape Pipe
            }
            element "#Database" {
                shape Cylinder
            }
            element "#ApiService" {
                shape Hexagon
            }
            element "#WebBrowser" {
                shape WebBrowser
            }
            element "#Device" {
                shape MobileDevicePortrait
            }
        }
    }
}