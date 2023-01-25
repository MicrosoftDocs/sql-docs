---
title: SqlDependency in an ASP.NET application
description: Demonstrates how to use query notifications via SqlDependency from an ASP.NET application.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# SqlDependency in an ASP.NET application

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

The example in this section shows how to use <xref:Microsoft.Data.SqlClient.SqlDependency> indirectly by using the ASP.NET <xref:System.Web.Caching.SqlCacheDependency> object. The <xref:System.Web.Caching.SqlCacheDependency> object uses a <xref:Microsoft.Data.SqlClient.SqlDependency> to listen for notifications and correctly update the cache.

> [!NOTE]
> The sample code assumes that you have enabled query notifications by executing the scripts in [Enabling query notifications](enable-query-notifications.md).

## About the sample application

The sample application uses a single ASP.NET Web page to display product information from the **AdventureWorks** SQL Server database in a <xref:System.Web.UI.WebControls.GridView> control. When the page loads, the code writes the current time to a <xref:System.Web.UI.WebControls.Label> control. It then defines a <xref:System.Web.Caching.SqlCacheDependency> object and sets properties on the <xref:System.Web.Caching.Cache> object to store the cache data for up to three minutes. The code then connects to the database and retrieves the data. When the page is loaded and the application is running ASP.NET will retrieve data from the cache, which you can verify by noting that the time on the page doesn't change. If the data being monitored changes, ASP.NET invalidates the cache and repopulates the `GridView` control with fresh data, updating the time displayed in the `Label` control.

## Creating the sample application

Follow these steps to create and run the sample application:

1. Create a new ASP.NET Web site.

2. Add a <xref:System.Web.UI.WebControls.Label> and a <xref:System.Web.UI.WebControls.GridView> control to the Default.aspx page.

3. Open the page's class module and add the following directives:

    ```csharp
    using Microsoft.Data.SqlClient;
    using System.Web.Caching;
    ```

4. Add the following code in the page's `Page_Load` event:

    [!code-csharp[DataWorks SqlDependency_Start#1](~/../sqlclient/doc/samples/SqlDependency_Start.cs#1)]

5. Add two helper methods, `GetConnectionString` and `GetSQL`. The connection string defined uses integrated security. Verify that the account you're using has the necessary database permissions and that the sample database, `AdventureWorks`, has notifications enabled.

    [!code-csharp[DataWorks SqlDependency_Start#2](~/../sqlclient/doc/samples/SqlDependency_Start.cs#2)]

### Testing the application

The application caches the data displayed on the Web form and refreshes it every three minutes if there's no activity. If a change occurs to the database, the cache is refreshed immediately. Run the application from Visual Studio, which loads the page into the browser. The cache refresh time displayed indicates when the cache was last refreshed. Wait three minutes, and then refresh the page, causing a postback event to occur. The time displayed on the page has changed. If you refresh the page in less than three minutes, the time displayed on the page will remain the same.

Now update the data in the database using a Transact-SQL UPDATE command and refresh the page. The time displayed now indicates the cache was refreshed with the new data from the database. Although the cache is updated, the time displayed on the page doesn't change until a postback event occurs.

## Next steps

- [Query notifications in SQL Server](query-notifications-sql-server.md)
