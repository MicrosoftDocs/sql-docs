---
title: Load with Integration Services
description: Provides reference and deployment information for loading data into Parallel Data Warehouse (PDW) by using SQL Server Integration Services (SSIS) packages.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Load data with Integration Services to Parallel Data Warehouse
Provides reference and deployment information for loading data into SQL Server Parallel Data Warehouse by using SQL Server Integration Services (SSIS) packages.  
  
<!-- MISSING LINKS

## <a name="BeforeBegin"></a>Before You Begin  
Before you can start loading data, use the following topics to install the Integration Services Destination Adapters and to create an Integration Services package that connects SQL Server PDW:  


-   [Install Integration Services destination adapters](install-integration-services-destination-adapters.md)  
  
-   [Connect With Integration Services for loading](connect-with-ssis-for-loading.md)  
  
For general information about developing Integration Services packages, see [Designing and Implementing Packages (Integration Services)](https://msdn.microsoft.com/library/ms141091\(v=sql11\).aspx) on MSDN.  

-->
  
## <a name="Basics"></a>Basics  
Integration Services is the component of SQL Server for high-performance extraction, transformation, and loading (ETL) of data, and is commonly used to populate and update a data warehouse.  
  
The PDW Destination Adapter is an Integration Services component that lets you load data into PDW by using Integration Services dtsx packages. In a package workflow for SQL ServerPDW, you can load and merge data from multiple sources and load data to multiple destinations. The loads occur in parallel, both within a package and among multiple packages running concurrently, up to a maximum of 10 loads running in parallel on the same appliance.  
  
In addition to the tasks described in this topic, you can use other features of Integration Services to filter, transform, analyze, and cleanse your data before loading it into the data warehouse. You can also enhance the workflow of the package by running SQL statements, running child packages, or sending mail.  
  
For complete documentation of Integration Services, see [SQL Server Integration Services](../integration-services/sql-server-integration-services.md).  
  
## <a name="HowToDeployPackage"></a>Methods for running an Integration Services package  
Use one of these methods to run an Integration Services package.  
  
### Run from SQL Server 2008 R2 Business Intelligence Development Studio (BIDS)  
To run the package from within BIDS, right-click on your package and choose **Execute Package**.  
  
By default, BIDS runs packages using 64-bit binaries. This is determined by the **Run64BitRuntime** package property. To set this property, go to **Solution Explorer**, right-click on your project and choose **Properties**. On the **Integration Services Property Pages**, go to **Configuration Properties** and select **Debugging**. You will see the **Run64BitRuntime** property under the **Debug Options**. To use 32-bit runtimes, set this to **False**. To use 64-bit runtimes, set this to **True**.  
  
### Run from SQL Server 2012 SQL Server Data Tools  
To run the package from within SQL Server Data Tools, right-click on your package and choose **Execute Package**.  
  
### Run From PowerShell  
To run the package from Windows PowerShell, using the **dtexec** utility: `dtexec /FILE <packagePath>`  
  
For example, `dtexec /FILE "C:\Users\User1\Desktop\Package.dtsx"`  
  
### Run From a Windows command prompt 
To run the package from a Windows command prompt, using the **dtexec** utility: `dtexec /FILE <packagePath>`  
  
For example: `dtexec /FILE "C:\Users\User1\Desktop\Package.dtsx"`  
  
## <a name="DataTypes"></a>Data types  
When using Integration Services to load data from a data source to a SQL Server PDW database, the data is first mapped from the source data to Integration Services data types. This allows data from multiple data sources to map to a common set of data types.  
  
Then the data is mapped from Integration Services to SQL Server PDW data types. For each SQL Server PDW data type, the following table lists the Integration Services data types that can be converted to the SQL Server PDW data type.  
  
|PDW data type|Integration Services data types(s) that map to PDW data type|  
|---------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------|  
|BIT|DT_BOOL|  
|BIGINT|DT_I1, DT_I2, DT_I4, DT_I8, DT_UI1, DT_UI2, DT_UI4|  
|CHAR|DT_STR|  
|DATE|DT_DBDATE|  
|DATETIME|DT_DATE, DT_DBDATE, DT_DBTIMESTAMP, DT_DBTIMESTAMP2|  
|DATETIME2|DT_DATE, DT_DBDATE, DT_DBTIMESTAMP, DT_DBTIMESTAMP2|  
|DATETIMEOFFSET|DT_WSTR|  
|DECIMAL|DT_DECIMAL, DT_I1, DT_I2, DT_I4, DT_I4, DT_I8, DT_NUMERIC, DT_UI1, DT_UI2, DT_UI4, DT_UI8|  
|FLOAT|DT_R4, DT_R8|  
|INT|DT_I1, DTI2, DT_I4, DT_UI1, DT_UI2|  
|MONEY|DT_CY|  
|NCHAR|DT_WSTR|  
|NUMERIC|DT_DECIMAL, DT_I1, DT_I2, DT_I4, DT_I8, DT_NUMERIC, DT_UI1, DT_UI2, DT_UI4, DT_UI8|  
|NVARCHAR|DT_WSTR, DT_STR|  
|REAL|DT_R4|  
|SMALLDATETIME|DT_DBTIMESTAMP2|  
|SMALLINT|DT_I1, DT_I2, DT_UI1|  
|SMALLMONEY|DT_R4|  
|TIME|DT_WSTR|  
|TINYINT|DT_I1|  
|VARBINARY|DT_BYTES|  
|VARCHAR|DT_STR|  
  
**Limited support for data type precision**  
  
PDW generates a validation error if you map a DT_NUMERIC or DT_DECIMAL input column that contains a value with precision greater than 28.  
  
**Unsupported Data Types**  
  
SQL Server PDW does not support the following Integration Services data types:  
  
-   DT_DBTIMESTAMPOFFSET  
  
-   DT_DBTIME2  
  
-   DT_GUID  
  
-   DT_IMAGE  
  
-   DT_NTEXT  
  
-   DT_TEXT  
  
To load columns that contain data of these types into SQL Server PDW, you must add a Data Conversion transformation upstream in the data flow to convert the data to a compatible data type.  
  
## Permissions  
To run an Integration Services load package, you need:  
  
-   LOAD permission on the database.  
  
-   Applicable INSERT, UPDATE, DELETE permissions on the destination table.  
  
-   If a staging database is used, CREATE permission on the staging database. This is for creating a temporary table.  
  
-   If no staging database is used, CREATE permission on the destination database. This is for creating a temporary table.  
  
## <a name="GenRemarks"></a>General remarks  
When an Integration Services package has multiple SQL Server PDW destinations running and one of the connections is terminated, Integration Services stops pushing data to all of the SQL Server PDW destinations.  
  
## <a name="Limits"></a>Limitations and restrictions  
For an Integration Services package, the number of SQL Server PDW destinations for the same data source is limited by the maximum number of active loads. The maximum is pre-configured and is not user-configurable. 

<!-- MISSING LINKS
For the maximum number of loads and queued loads per appliance, see [Minimum and maximum values](minimum-and-maximum-values.md).  
-->
  
Each Integration Services package destination for the same data source counts as one load when the package is running. For example, suppose the maximum active loads is 10. The package will not run if it attempts to open 11 or more destinations for the same data source.  
  
Multiple packages can run concurrently as long as each package does not use more than the maximum active loads. For example, if the maximum active loads is 10, you can concurrently run two packages that each use 10 destinations. One package will run while the other one waits in the load queue.  
  
If the number of loads in the load queue exceeds the maximum queued loads, the package will not run. For example, if the maximum number of loads is 10 per appliance and the maximum number of queued loads is 40 per appliance, you can concurrently run five Integration Services packages that each open 10 destinations. If you try to run a sixth package, it will not run.  

> [!IMPORTANT]
> Using an OLE DB data source in SSIS with the PDW destination adapter, can cause data corruption if the source table contains char and varchar columns with SQL collations. We recommend using an ADO.NET source if the source table contains char or varchar columns with SQL collations. 

  
## <a name="Locks"></a>Locking behavior  
When loading data with Integration Services, SQL ServerPDW uses row-level locks to update data in the destination table. This means that each row is locked for read and write while it is being updated. The rows in the destination table are not locked while the data is loaded into the staging table.  
  
## <a name="Examples"></a>Examples  
  
### <a name="Walkthrough"></a>A. Simple load from flat file  
The following walkthrough demonstrates a simple data load using Integration Services to load flat file data to a SQL Server PDW appliance.  This example assumes that Integration Services has already been installed on the client machine, and the SQL Server PDW destination has been installed, as described above.  
  
In this example we will load into the `Orders` table, which has the following DDL. The `Orders` table is part of the `LoadExampleDB` database.  
  
```sql  
CREATE TABLE LoadExampleDB.dbo.Orders (  
   id INT,  
   city varchar(25),  
   lastUpdateDate DATE,  
   orderDate DATE)  
;  
```  
  
Here is the load data:  
  
```  
id        city           lastUpdateDate     orderdate  
--------- -------------- ------------------ ----------  
1         Seattle        2010-05-01         2010-01-01  
2         Denver         2002-06-25         1999-01-02  
```  
  
In preparation for the load, create the flat file `exampleLoad.txt`, containing the load data:  
  
```  
id,city,lastUpdateDate,orderDate  
1,Seattle,2010-05-01,2010-01-01  
2,Denver,2002-06-25,1999-01-02  
```  
  
First, create an Integration Services package by performing these steps:  
  
1.  In SQL Server Data Tools \(SSDT\), select **File**, **New**, and then **Project**. Select **Integration Services Project** from the options listed. Name this project `ExampleLoad`, and click **OK**.  
  
2.  Click the **Control Flow** tab and then drag the **Data Flow Task** from the **Toolbox** to the **Control Flow** pane.  
  
3.  Click the **Data Flow** tab and then drag **Flat File Source** from the **Toolbox** to the **Data Flow** pane. Double-click the box you just created to open the **Flat File Source Editor**.  
  
4.  Click **Connection Manager** and then click **New**.  
  
5.  In the **Connection manager name** box, enter a friendly name for the connection. For this example, `Example Load Flat File CM`.  
  
6.  Click **Browse** and select the `ExampleLoad.txt` file from the local machine.  
  
7.  Since the flat file contains a row with column names, click the **Column names in the first data row** box.  
  
8.  Click **Columns** in the left column, and preview the data that will be loaded to make sure the column names and data were interpreted correctly.  
  
9. Click **Advanced** in the left column. Click on each column name to review the data type that has been associated with the data. Type changes in the box so that the data types of the loaded data will be compatible with the destination column types.  
  
10. Click **OK** to save your connection manager.  
  
11. Click **OK** to exit the **Flat File Source Editor**.  
  
Specify the destination for the data flow.  
  
1.  Drag the **SQL Server PDW Destination** from the **Toolbox** to the **Data Flow** pane.  
  
2.  Double-click the box you just created to load the **SQL Server PDW Destination Editor**.  
  
3.  Click the down arrow next to **Connection Manager**.  
  
4.  Select **Create a New Connection**.  
  
5.  Fill in the information for the server, user, password, and destination database with information specific to your appliance. (Examples are shown below). Then click **OK**.  
  
    For InfiniBand connections, **Server name**: Enter \<appliance-name\>-SQLCTL01,17001.  
  
    For Ethernet connections, **Server name**: Enter the IP address of the Control node cluster, comma, port 17001. For example, 10.192.63.134,17001.  
  
    **User:**`user1`  
  
    **Password:**`password1`  
  
    **Destination Database:**`LoadExampleDB`  
  
6.  Select the destination table: `Orders`.  
  
7.  Select **Append** as the loading mode and click **OK**.  
  
Specify the data flow from source to destination.  
  
1.  On the **Data Flow** pane, drag the green arrow from the **Flat File Source** box to the **SQL Server PDW Destination** box.  
  
2.  Double-click the **SQL Server PDW Destination** box so that you see the **SQL Server PDW Destination Editor** again. You should see the column names from the flat file on the left, under **Unmapped Input Columns**. You should see the column names from the destination table on the right, under **Unmapped Destination Columns**. Map the columns by dragging or double-clicking the matching column names in the **Unmapped Input Columns** and **Unmapped Destination Columns** lists to the **Mapped Columns** box. Click **OK** to save your settings.  
  
3.  Save the package by clicking **Save** in the **File** menu.  
  
Run the package on your computer Integration Services.  
  
1.  In the Integration Services**Solution Explorer** (right column), right-click `Package.dtsx` and select **Execute**.  
  
2.  The package will run and the progress plus any errors will be shown on the **Progress** pane. Use a SQL client to confirm the load, or monitor the load via the SQL Server PDW Admin Console.  
  
## See Also  
[Create a script task that uses the SSIS PDW destination adapter](create-ssis-script-task-using-pdw-destination-adapter.md)  
[SQL Server Integration Services](../integration-services/sql-server-integration-services.md)  
[Designing and Implementing Packages (Integration Services)](https://msdn.microsoft.com/library/ms141091\(v=sql11\).aspx)  
[Tutorial: Creating a Basic Package Using a Wizard](https://technet.microsoft.com/library/ms365330\(v=sql11\).aspx)  
[Getting Started (Integration Services)](https://go.microsoft.com/fwlink/?LinkId=202412)  
[Dynamic Package Generation Sample](https://apexandbeyond.wordpress.com/2017/03/15/dynamic-package-xml-generation/)  
[Designing Your SSIS Packages for Parallelism (SQL Server Video)](/previous-versions/sql/sql-server-2008/dd795221(v=sql.100))  
[Improving Incremental Loads with Change Data Capture](../integration-services/change-data-capture/change-data-capture-ssis.md)  
[Slowly Changing Dimension Transformation](../integration-services/data-flow/transformations/slowly-changing-dimension-transformation.md)  
[Bulk Insert Task](../integration-services/control-flow/bulk-insert-task.md)  
  
<!-- MISSING LINKS
[Grant permissions to load data](grant-permissions-to-load-data.md)  
[Common metadata query examples](metadata-query-examples.md)
-->
