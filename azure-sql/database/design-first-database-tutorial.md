---
title: "Tutorial: Design your first relational database using SSMS"
description: Learn to design your first relational database in Azure SQL Database using SQL Server Management Studio.
author: dzsquared
ms.author: drskwier
ms.reviewer: wiassaf, mathoma, v-masebo
ms.date: 01/26/2024
ms.service: azure-sql-database
ms.subservice: development
ms.topic: tutorial
---
# Tutorial: Design a relational database in Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database is a relational database-as-a-service (DBaaS) in the Microsoft Azure. In this tutorial, you learn how to:

> [!div class="checklist"]
>
> - Connect to the database
> - Create tables with T-SQL commands
> - Bulk load data with BCP
> - Query data with T-SQL commands

> [!NOTE]
> For the purpose of this tutorial, we are using Azure SQL Database. You could also use a pooled database in an elastic pool or a SQL Managed Instance. For connectivity to a SQL Managed Instance, see these SQL Managed Instance quickstarts: [Quickstart: Configure Azure VM to connect to an Azure SQL Managed Instance](../managed-instance/connect-vm-instance-configure.md) and [Quickstart: Configure a point-to-site connection to an Azure SQL Managed Instance from on-premises](../managed-instance/point-to-site-p2s-configure.md).

## Prerequisites

- Use [SQL Server Management Studio](/sql/ssms/sql-server-management-studio-ssms) (latest version) or the [Azure portal Query Editor for Azure SQL Database](query-editor.md).
    - You can also follow this same tutorial using [Azure Data Studio (ADS)](design-first-database-azure-data-studio.md).
- [BCP and SQLCMD](https://www.microsoft.com/download/details.aspx?id=36433) (latest version).
- If you don't have an Azure subscription, [create a free account](https://azure.microsoft.com/free/) before you begin.
- If you don't already have an Azure SQL Database created, visit [Quickstart: Create a single database](single-database-create-quickstart.md). Look for the option to use your offer to [try Azure SQL Database for free (preview)](free-offer.md).

## Sign in to the Azure portal

Sign in to the [Azure portal](https://portal.azure.com/).

## Create a server-level IP firewall rule

Azure SQL Database creates an IP firewall at the server-level. This firewall prevents external applications and tools from connecting to the server and any databases on the server unless a firewall rule allows their IP through the firewall. To enable external connectivity to your database, you must first add an IP firewall rule for your IP address (or IP address range). Follow these steps to create a [server-level IP firewall rule](firewall-configure.md).

> [!IMPORTANT]
> Azure SQL Database communicates over port 1433. If you are trying to connect to this service from within a corporate network, outbound traffic over port 1433 may not be allowed by your network's firewall. If so, you cannot connect to your database unless your administrator opens port 1433.

1. After the deployment completes, select **SQL databases** from the Azure portal menu or search for and select *SQL databases* from any page.  

1. Select *yourDatabase* on the **SQL databases** page. The overview page for your database opens, showing you the fully qualified **Server name** (such as `contosodatabaseserver01.database.windows.net`) and provides options for further configuration.

   :::image type="content" source="./media/design-first-database-tutorial/server-name.png" alt-text="Screenshot of the Azure portal, database overview page with the server name highlighted.":::

1. Copy this fully qualified server name for use to connect to your server and databases from SQL Server Management Studio.

1. Select **Networking** under **Settings**. Choose the **Public Access** tab, and then select **Selected networks** under **Public network access** to display the **Firewall rules** section. 

   :::image type="content" source="./media/design-first-database-tutorial/server-firewall-rule.png" alt-text="Screenshot of the Azure portal, networking page, showing where to set the server-level IP firewall rule.":::

1. Select **Add your client IPv4** on the toolbar to add your current IP address to a new IP firewall rule. An IP firewall rule can open port 1433 for a single IP address or a range of IP addresses.

1. Select **Save**. A server-level IP firewall rule is created for your current IP address opening port 1433 on the server.

1. Select **OK** and then close the **Firewall settings** page.

Your IP address can now pass through the IP firewall. You can now connect to your database using SQL Server Management Studio or another tool of your choice. Be sure to use the server admin account you created previously.

> [!IMPORTANT]
> By default, access through the SQL Database IP firewall is enabled for all Azure services. Select **OFF** on this page to disable for all Azure services.

## Connect to the database

Azure SQL databases exist inside logical SQL servers. Can connect to the logical SQL server's `master` using a login, then connect to your database. Or, using a [contained user](/sql/relational-databases/security/contained-database-users-making-your-database-portable?view=azuresqldb-current&preserve-view=true#contained-database-user-model), you can connect directly to your Azure SQL database.

## [SQL Server Management Studio](#tab/ssms)

Use [SQL Server Management Studio](/sql/ssms/sql-server-management-studio-ssms) to connect to your Azure SQL database.

1. Open SQL Server Management Studio.
1. In the **Connect to Server** dialog box, enter the following information. Leave other options as default.

   | Setting       | Suggested value | Description |
   | ------------ | ------------------ | ------------------------------------------------- |
   | **Server type** | Database engine | This value is required. |
   | **Server name** | The fully qualified Azure SQL Database logical server name | For example, `your_logical_azure_sql_server.database.windows.net`. |
   | **Authentication** | SQL Server Authentication | Use SQL Server Authentication to enter a user name and password. |
   | | Microsoft Entra authentication | To connect using Microsoft Entra ID, if you're the Microsoft Entra server admin, choose **Microsoft Entra MFA**. For more information, see [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md).|
   | **Login** | The server admin account | If using SQL Server Authentication, the account that you specified when you created the server. |
   | **Password** | The password for your server admin account | If using SQL Server Authentication, the password that you specified when you created the server. |

   :::image type="content" source="media\design-first-database-tutorial\connect.png" alt-text="Screenshot of the connect to an Azure SQL Database logical server server dialog box in SQL Server Management Studio (SSMS).":::

1. Select **Options** in the **Connect to server** dialog box. In the **Connect to database** section, enter *yourDatabase* to connect to this database.

    :::image type="content" source="media\design-first-database-tutorial\options-connect-to-db.png" alt-text="Screenshot of the options tab of the connect to server dialog box in SQL Server Management Studio (SSMS).":::

1. Select **Connect**. The **Object Explorer** window opens in SSMS.

1. In **Object Explorer**, expand **Databases** and then expand *yourDatabase* to view the objects in the sample database.

   :::image type="content" source="media\design-first-database-tutorial\connected.png" alt-text="Screenshot of SQL Server Management Studio (SSMS) showing database objects in object explorer.":::

1. In **Object Explorer**, right-click *yourDatabase* and select **New Query**. A blank query window opens that is connected to your database.

## [Azure portal Query editor](#tab/queryeditor)

Use the [Azure portal Query editor for Azure SQL Database](query-editor.md) to connect to your Azure SQL database.

1. Navigate to your SQL database in the Azure portal. For example, visit [your Azure SQL dashboard](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql).

1. On your SQL database **Overview** page in the [Azure portal](https://portal.azure.com), select **Query editor (preview)** from the left menu.

   :::image type="content" source="media\design-first-database-tutorial\find-query-editor.png" alt-text="Screenshot that shows selecting query editor.":::

1. On the sign-in screen under **Welcome to SQL Database Query Editor**, provide credentials to connect to the database. You can connect using SQL or Microsoft Entra authentication.

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
> To learn more about writing SQL queries, visit [Tutorial: Write Transact-SQL statements](/sql/t-sql/tutorial-writing-transact-sql-statements).

## Related content

- [Tutorial: Design a relational database in Azure SQL Database using Azure Data Studio (ADS)](design-first-database-azure-data-studio.md)
- [Try Azure SQL Database for free (preview)](free-offer.md)
- [What's new in Azure SQL Database?](doc-changes-updates-release-notes-whats-new.md)
- [Configure and manage content reference - Azure SQL Database](how-to-content-reference-guide.md)
- [Plan and manage costs for Azure SQL Database](cost-management.md)

> [!TIP]
> **Ready to start developing an .NET application?** This free Learn module shows you how to [Develop and configure an ASP.NET application that queries an Azure SQL Database](/training/modules/develop-app-that-queries-azure-sql/), including the creation of a simple database.

## Next step

Advance to the next tutorial to learn about designing a database using Visual Studio and C#.

> [!div class="nextstepaction"]
> [Design a relational database within Azure SQL Database C# and ADO.NET](design-first-database-csharp-tutorial.md)
