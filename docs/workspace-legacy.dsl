workspace {
    

    name "Solution Architecture Diagram"
    description "Draw the architectural diagram of the solution as if you were designing it from scratch."

    model {
        Database = softwareSystem "Central Database" "" "#Database"
        CouriersMobileApplication = softwareSystem "Mobile Application" "" "#External"

        WarehouseApplication = softwareSystem "Warehouse: Storage & Delivery" {
            desktopUI = container "DesktopUI"
            WarehouseAuthentication = container "Authentication"
            WarehouseBusinessLogicLayer = container "BLL"
            WarehouseDataAccessLayer = container "DAL"
        }
        BackOfficeApplication = softwareSystem "BackOffice: Orders Processing" {
            OfficeDesktopUI = container "Web UI"
            BackOfficeAuthentication = container "Authentication"
            BackOfficeBusinessLogicLayer = container "BLL"
            BackOfficeDataAccessLayer = container "DAL"
        }
        OnlineShopApplication = softwareSystem "Online Shop: Orders Placement" {
            ShopOrdersWebUI = container "Web UI" "ASP.NET MVC"
            OnlineShopAuthentication = container "Authentication"
            OnlineShopBusinessLogicLayer = container "BLL"
            OnlineShopDataAccessLayer = container "DAL"
        }
        MobileApplicationService = softwareSystem "Mobile: Orders Placement" "" "ApiService" {
            MobileOrdersWebUI = container "Web API" "ASP.NET MVC"
            MobileServiceAuthentication = container "Authentication"
            MobileServiceBusinessLogicLayer = container "BLL"
            MobileServiceDataAccessLayer = container "DAL"
        }
    
        # Database interactions
        WarehouseApplication -> Database "Makes SQL Queries"
        BackOfficeApplication -> Database "Makes SQL Queries"
        OnlineShopApplication -> Database "Makes SQL Queries"
        MobileApplicationService -> Database "Makes SQL Queries"
        
        # Mobile Application interactions
        CouriersMobileApplication -> MobileApplicationService ""
    }

    views {
    
        styles {
            theme default

            element "#External" {
                background #999999
                color #ffffff
            }
            element "#Database" {
                shape Cylinder
            }
            element "#ApiService" {
                shape RoundedBox
            }
        }

    }

}