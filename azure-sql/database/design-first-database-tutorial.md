---
title: "Tutorial: Design your first relational database using SSMS"
description: Learn to design your first relational database in Azure SQL Database using SQL Server Management Studio.
author: dzsquared
ms.author: drskwier
ms.reviewer: wiassaf, mathoma, v-masebo
ms.date: 01/26/2024
ms.service: sql-database
ms.subservice: development
ms.topic: tutorial
ms.custom:
  - sqldbrb=1
---
# Tutorial: Design a relational database in Azure SQL Database using SSMS
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database is a relational database-as-a-service (DBaaS) in the Microsoft Azure. In this tutorial, you learn how to:

> [!div class="checklist"]
>
> - Create a database using the Azure portal*
> - Set up a server-level IP firewall rule using the Azure portal
> - Connect to the database with SSMS
> - Create tables with SSMS
> - Bulk load data with BCP
> - Query data with SSMS

> [!NOTE]
> For the purpose of this tutorial, we are using Azure SQL Database. You could also use a pooled database in an elastic pool or a SQL Managed Instance. For connectivity to a SQL Managed Instance, see these SQL Managed Instance quickstarts: [Quickstart: Configure Azure VM to connect to an Azure SQL Managed Instance](../managed-instance/connect-vm-instance-configure.md) and [Quickstart: Configure a point-to-site connection to an Azure SQL Managed Instance from on-premises](../managed-instance/point-to-site-p2s-configure.md).

## Prerequisites

- Use [SQL Server Management Studio](/sql/ssms/sql-server-management-studio-ssms) (latest version) or the [Azure portal Query Editor for Azure SQL Database](query-editor.md).
- [BCP and SQLCMD](https://www.microsoft.com/download/details.aspx?id=36433) (latest version).
- If you don't have an Azure subscription, [create a free account](https://azure.microsoft.com/free/) before you begin.
- If you don't already have an Azure SQL Database created, we'll create one in this tutorial. Look for the option to use your offer to [try Azure SQL Database for free (preview)](free-offer.md).

## Sign in to the Azure portal

Sign in to the [Azure portal](https://portal.azure.com/).

## Create a blank database in Azure SQL Database

A database in Azure SQL Database is created with a defined set of compute and storage resources. The database is created within an [Azure resource group](/azure/active-directory-b2c/overview) and is managed using an [logical SQL server](logical-servers.md).

Follow these steps to create a blank database.

1. On the Azure portal menu or from the **Home** page, select **Create a resource**.
1. On the **New** page, select **Databases** in the Azure Marketplace section. Under **SQL Database**, select **Create**.

   :::image type="content" source="media\design-first-database-tutorial\create-empty-database.png" alt-text="Screenshot of the Azure portal, selecting a SQL Database from Azure Marketplace.":::

1. If you're eligible for the [Azure SQL Database for free](free-offer.md) offer, you'll see a banner and a button to **Apply offer**. Some options in the following steps will be simplified for you.
1. Fill out the **SQL Database** form with the following information, leaving other options as default.

    | Setting       | Suggested value | Description |
    | ------------ | ------------------ | ------------------------------------------------- |
    | **Subscription** | *yourSubscription*  | For details about your subscriptions, see [Subscriptions](https://account.windowsazure.com/Subscriptions). |
    | **Resource group** | *yourResourceGroup* | For valid resource group names, see [Naming rules and restrictions](/azure/architecture/best-practices/resource-naming). |
    | **Database name** | *yourDatabase* | For valid database names, see [Database identifiers](/sql/relational-databases/databases/database-identifiers). |
    | **Workload environment** | Development | Reduce cost by declaring using default settings for pre-production environments. |
    | **Select source** | Blank database | Specifies that a blank database should be created. |

1. Choose a server from the drop-down to use an existing server or select **Create new** to create and configure a new server. Either select an existing server or select **Create a new server** and fill out the **New server** form with the following information:

    | Setting       | Suggested value | Description |
    | ------------ | ------------------ | ------------------------------------------------- |
    | **Server name** | Any globally unique name | For valid server names, see [Naming rules and restrictions](/azure/architecture/best-practices/resource-naming). |
    | **Location** | Any valid location | For information about regions, see [Azure Regions](https://azure.microsoft.com/regions/). |
    | **Server admin login** | Any valid name | For valid login names, see [Database identifiers](/sql/relational-databases/databases/database-identifiers). |
    | **Password** | Any valid password | Your password must have at least eight characters and must use characters from three of the following categories: upper case characters, lower case characters, numbers, and non-alphanumeric characters. There are other limitations to the password string, see popup for more information. |

    :::image type="content" source="media\design-first-database-tutorial\create-database-server.png" alt-text="Screenshot of the Azure portal, creating a logical server for Azure." lightbox="media\design-first-database-tutorial\create-database-server.png":::

1. Select **Select**.
1. Select **Pricing tier** to specify the service tier, the number of DTUs or vCores, and the amount of storage. You might explore the options for the number of DTUs/vCores and storage that is available to you for each service tier. For this tutorial, use either the [Azure SQL Database for free](free-offer.md) offer or choose a **Basic DTU** tier for lowest cost.

    After selecting the service tier, the number of DTUs or vCores, and the amount of storage, select **Apply**.

1. Enter a **Collation** for the blank database (for this tutorial, use the default value). For more information about collations, see [Collations](/sql/t-sql/statements/collations).

1. Now that you've completed the **SQL Database** form, select **Create** to provision the database. This step might take a few minutes.

1. On the toolbar, select **Notifications** to monitor the deployment process. 

## Create a server-level IP firewall rule

Azure SQL Database creates an IP firewall at the server-level. This firewall prevents external applications and tools from connecting to the server and any databases on the server unless a firewall rule allows their IP through the firewall. To enable external connectivity to your database, you must first add an IP firewall rule for your IP address (or IP address range). Follow these steps to create a [server-level IP firewall rule](firewall-configure.md).

> [!IMPORTANT]
> Azure SQL Database communicates over port 1433. If you are trying to connect to this service from within a corporate network, outbound traffic over port 1433 might not be allowed by your network's firewall. If so, you cannot connect to your database unless your administrator opens port 1433.

1. After the deployment completes, select **SQL databases** from the Azure portal menu or search for and select *SQL databases* from any page.  

1. Select *yourDatabase* on the **SQL databases** page. The overview page for your database opens, showing you the fully qualified **Server name** (such as `<your server name>.database.windows.net`) and provides options for further configuration.

   :::image type="content" source="media\design-first-database-tutorial\server-name.png" alt-text="Screenshot of the Azure portal, database overview page with the server name highlighted." lightbox="media\design-first-database-tutorial\server-name.png":::

1. Copy this fully qualified server name to your clipboard.

1. Select **Networking** under **Settings**. Choose the **Public Access** tab, and then select **Selected networks** under **Public network access** to display the **Firewall rules** section.

   :::image type="content" source="media\design-first-database-tutorial\server-firewall-rule.png" alt-text="Screenshot of the Azure portal, networking page, showing where to set the server-level IP firewall rule." lightbox="media\design-first-database-tutorial\server-firewall-rule.png":::

1. Select **Add your client IPv4** on the toolbar to add your current IP address to a new IP firewall rule. An IP firewall rule can open port 1433 for a single IP address or a range of IP addresses.

1. Select **Save**. A server-level IP firewall rule is created for your current IP address opening port 1433 on the server.

1. Select **OK** and then close the **Firewall settings** page.

Your IP address can now pass through the IP firewall. You can now connect to your database using any tool of your choice.

> [!IMPORTANT]
> By default, access through the SQL Database IP firewall is enabled for all Azure services. Select **OFF** on this page to disable for all Azure services.

## Connect to the database

## [SQL Server Management Studio](#tab/ssms)

Use [SQL Server Management Studio](/sql/ssms/sql-server-management-studio-ssms) to connect to your Azure SQL database.

1. Open SQL Server Management Studio.
1. In the **Connect to Server** dialog box, enter the following information:

   | Setting       | Suggested value | Description |
   | ------------ | ------------------ | ------------------------------------------------- |
   | **Server type** | Database engine | This value is required. |
   | **Server name** | The fully qualified Azure SQL Database logical server name | For example, `your_logical_azure_sql_server.database.windows.net`. |
   | **Authentication** | SQL Server Authentication | Use SQL Server Authentication to enter a user name and password. |
   | | Microsoft Entra authentication | To connect using Microsoft Entra ID, if you're the Microsoft Entra server admin, use the **Microsoft Entra MFA** Authentication option. For more information, see [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md).|
   | **Login** | The server admin account | If using SQL Server Authentication, the account that you specified when you created the server. |
   | **Password** | The password for your server admin account | If using SQL Server Authentication, the password that you specified when you created the server. |

   :::image type="content" source="media\design-first-database-tutorial\connect.png" alt-text="Screenshot of the connect to an Azure SQL Database logical server server dialog box in SQL Server Management Studio (SSMS).":::

1. Select **Options** in the **Connect to server** dialog box. In the **Connect to database** section, enter *yourDatabase* to connect to this database.

    :::image type="content" source="media\design-first-database-tutorial\options-connect-to-db.png" alt-text="Screenshot of the options tab of the connect to server dialog box in SQL Server Management Studio (SSMS).":::

1. Select **Connect**. The **Object Explorer** window opens in SSMS.

1. In **Object Explorer**, expand **Databases** and then expand *yourDatabase* to view the objects in the sample database.

   :::image type="content" source="media\design-first-database-tutorial\connected.png" alt-text="Screenshot of SQL Server Management Studio (SSMS) showing database objects in object explorer.":::

1. In **Object Explorer**, right-click *yourDatabase* and select **New Query**. A blank query window opens that is connected to your database.

## [Azure portal Query Editor](#tab/queryeditor)

Use the [Azure portal Query Editor for Azure SQL Database](query-editor.md) to connect to your Azure SQL database.

1. Navigate to your SQL database in the Azure portal. For example, visit [your Azure SQL dashboard](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql).

   Azure SQL databases exist inside logical SQL servers. Can connect to the logical SQL server using a login, then connect to your database. Or, using a [contained user](/sql/relational-databases/security/contained-database-users-making-your-database-portable?view=azuresqldb-current#contained-database-user-model), you can connect directly to your Azure SQL Database.

1. On your SQL database **Overview** page in the [Azure portal](https://portal.azure.com), select **Query editor (preview)** from the left menu.

   :::image type="content" source="media\design-first-database-tutorial\find-query-editor.png" alt-text="Screenshot that shows selecting query editor.":::

1. On the sign-in screen under **Welcome to SQL Database Query Editor**, you can connect using SQL or Microsoft Entra authentication.

   - To connect with SQL authentication, under **SQL server authentication**, enter a **Login** and **Password** for a user that has access to the database, and then select **OK**. You can always use the login and password for the server admin.

     :::image type="content" source="media\design-first-database-tutorial\login-menu.png" alt-text="Screenshot from the Azure portal showing sign-in with SQL authentication." lightbox="media\design-first-database-tutorial\login-menu.png":::

   - To connect using Microsoft Entra ID, if you're the Microsoft Entra server admin, select **Continue as \<your user or group ID>**. If sign-in is unsuccessful, try refreshing the page.

     :::image type="content" source="media\design-first-database-tutorial\query-editor-entra-login.png" alt-text="Screenshot from the Azure portal showing sign-in with Microsoft Entra authentication." lightbox="media\design-first-database-tutorial\query-editor-entra-login.png":::

1. A new query window opens, ready to accept T-SQL commands. In the object explorer, you can expand folders for **Tables**, **Views**, and **Stored procedures**.

---

## Create tables in your database

Create four tables that model a student management system for universities using [Transact-SQL](/sql/t-sql/language-reference):

- `Person`
- `Course`
- `Student`
- `Credit`

The following diagram shows how these tables are related to each other. Some of these tables reference columns in other tables. For example, the `Student` table references the `PersonId` column of the `Person` table. Study the diagram to understand how the tables in this tutorial are related to one another. For an in-depth look at how to create effective normalized database tables, see [Designing a Normalized Database](/previous-versions/tn-archive/cc505842(v=technet.10)). For information about choosing data types, see [Data types](/sql/t-sql/data-types/data-types-transact-sql?view=azuresqldb-current&preserve-view=true). By default, tables are created in the default `dbo` schema, meaning the two-part name of a table will be `dbo.Person`, for example.

> [!NOTE]
> You can also use the [table designer in SQL Server Management Studio](/sql/ssms/visual-db-tools/design-database-diagrams-visual-database-tools) to create and design your tables.

:::image type="content" source="media\design-first-database-tutorial\tutorial-database-tables.png" alt-text="Screenshot of the table designer in SQL Server Management Studio (SSMS) showing the table relationships.":::

1. In the query window, execute the following T-SQL query to create four tables in your database:

   ```sql
   -- Create Person table
   CREATE TABLE Person
   (
       PersonId INT IDENTITY PRIMARY KEY,
       FirstName NVARCHAR(128) NOT NULL,
       MiddelInitial NVARCHAR(10),
       LastName NVARCHAR(128) NOT NULL,
       DateOfBirth DATE NOT NULL
   )

   -- Create Student table
   CREATE TABLE Student
   (
       StudentId INT IDENTITY PRIMARY KEY,
       PersonId INT REFERENCES Person (PersonId),
       Email NVARCHAR(256)
   )

   -- Create Course table
   CREATE TABLE Course
   (
       CourseId INT IDENTITY PRIMARY KEY,
       Name NVARCHAR(50) NOT NULL,
       Teacher NVARCHAR(256) NOT NULL
   )

   -- Create Credit table
   CREATE TABLE Credit
   (
       StudentId INT REFERENCES Student (StudentId),
       CourseId INT REFERENCES Course (CourseId),
       Grade DECIMAL(5,2) CHECK (Grade <= 100.00),
       Attempt TINYINT,
       CONSTRAINT [UQ_studentgrades] UNIQUE CLUSTERED
       (
           StudentId, CourseId, Grade, Attempt
       )
   )
   ```

   :::image type="content" source="media\design-first-database-tutorial\create-tables.png" alt-text="Screenshot from SSMS showing the create tables script has been successfully executed." lightbox="media\design-first-database-tutorial\create-tables.png":::

1. Expand the **Tables** node under *yourDatabase* in the **Object Explorer** to see the four new tables you created.

## Load data into the tables

1. Create a folder called *sampleData* in your local workstation *Downloads* folder to store sample data for your database. For example, `c:\Users\<your user name>\Downloads`.

1. Right-click the following links and save them into the *sampleData* folder.

   - [SampleCourseData](https://github.com/microsoft/sql-server-samples/releases/download/sqldbtutorial/SampleCourseData)
   - [SamplePersonData](https://github.com/microsoft/sql-server-samples/releases/download/sqldbtutorial/SamplePersonData)
   - [SampleStudentData](https://github.com/microsoft/sql-server-samples/releases/download/sqldbtutorial/SampleStudentData)
   - [SampleCreditData](https://github.com/microsoft/sql-server-samples/releases/download/sqldbtutorial/SampleCreditData)

1. Open a new Windows command prompt window and navigate to the *sampleData* folder. For example, `cd c:\Users\<your user name>\Downloads`.

1. Execute the following `bcp` commands to insert sample data into the tables replacing the values for *server*, *database*, *user*, and *password* with the values for your environment.

   ```cmd
   bcp Course in SampleCourseData -S <server>.database.windows.net -d <database> -U <user> -P <password> -q -c -t ","
   bcp Person in SamplePersonData -S <server>.database.windows.net -d <database> -U <user> -P <password> -q -c -t ","
   bcp Student in SampleStudentData -S <server>.database.windows.net -d <database> -U <user> -P <password> -q -c -t ","
   bcp Credit in SampleCreditData -S <server>.database.windows.net -d <database> -U <user> -P <password> -q -c -t ","
   ```

You have now loaded sample data into the tables you created earlier.

## Query data

Execute the following T-SQL queries to retrieve information from the database tables.

This first query joins all four tables to find the students taught by 'Dominick Pope' who have a grade higher than 75%. In a query window, execute the following T-SQL query:

   ```sql
   -- Find the students taught by Dominick Pope who have a grade higher than 75%
   SELECT  person.FirstName, person.LastName, course.Name, credit.Grade
   FROM  Person AS person
       INNER JOIN Student AS student ON person.PersonId = student.PersonId
       INNER JOIN Credit AS credit ON student.StudentId = credit.StudentId
       INNER JOIN Course AS course ON credit.CourseId = course.courseId
   WHERE course.Teacher = 'Dominick Pope'
       AND Grade > 75;
   ```

This query joins all four tables and finds the courses in which 'Noe Coleman' has ever enrolled. In a query window, execute the following T-SQL query:

   ```sql
   -- Find all the courses in which Noe Coleman has ever enrolled
   SELECT  course.Name, course.Teacher, credit.Grade
   FROM  Course AS course
       INNER JOIN Credit AS credit ON credit.CourseId = course.CourseId
       INNER JOIN Student AS student ON student.StudentId = credit.StudentId
       INNER JOIN Person AS person ON person.PersonId = student.PersonId
   WHERE person.FirstName = 'Noe'
       AND person.LastName = 'Coleman';
   ```

> [!TIP]
> To learn more about writing SQL queries, visit [Tutorial: Write Transact-SQL statements](../../docs/t-sql/tutorial-writing-transact-sql-statements.md).

## Related content

In this tutorial, you learned many basic database tasks. You learned how to:

> [!div class="checklist"]
>
> - Create a database using the Azure portal*
> - Set up a server-level IP firewall rule using the Azure portal
> - Connect to the database with SSMS
> - Create tables with SSMS
> - Bulk load data with BCP
> - Query data with SSMS

> [!TIP]
> **Ready to start developing an .NET application?** This free Learn module shows you how to [Develop and configure an ASP.NET application that queries an Azure SQL Database](/training/modules/develop-app-that-queries-azure-sql/), including the creation of a simple database.

## Next step

Advance to the next tutorial to learn about designing a database using Visual Studio and C#.

> [!div class="nextstepaction"]
> [Design a relational database within Azure SQL Database C# and ADO.NET](design-first-database-csharp-tutorial.md)
