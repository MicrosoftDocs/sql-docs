---
title: "Creating Sessions and Workflows in Informatica (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a6e4e48d-66e8-4c3e-a185-afcb71556db4
caps.latest.revision: 9
author: BarbKess
---
# Creating Sessions and Workflows in Informatica (SQL Server PDW)
Use the Informatica Workflow Manager to create workflows and sessions, and to configure the session extensions.  
  
### Creating a Workflow  
  
1.  Open the Informatica Workflow Manager and connect to the desired repository.  
  
2.  Click on **Workflow Designer**, click **Workflow**, click **Designer**, and then click **Create**.  
  
### Creating a session  
  
1.  Open the Informatica Workflow Manager and connect to the desired repository.  
  
2.  Click on **Task Developer**, click **Tasks**, and then click **Create**.  
  
3.  Assign a mapping.  
  
## Configuring sessions  
After creating a session, use the **Mapping** tab to configure properties of the session.  
  
1.  Click on **Task Developer**, click **Tasks**, select a task, and then click **Edit**.  
  
2.  In the **Edit Tasks** page, click the **Mapping** tab.  
  
#### Configure the source connection  
  
1.  In the left pane, expand **Sources**, and select a source.  
  
2.  In the **Connections** area, configure the connection.  
  
    Type  
    Select **Relational**.  
  
    Value  
    Provide an appropriate connection name.  
  
    Connections  
    Select **Microsoft SQL Server PDW**.  
  
![APS Informatica Session Extensions dialog box](../sqlpdw/media/APS_Informatica_Session_Extensions.png "APS_Informatica_Session_Extensions")  
  
#### Configure the target connection  
  
1.  In the left pane, expand **Targets**, and select a target.  
  
2.  In the **Writers** area, select the desired Writer (PDW Connector Writer or Loader).  
  
3.  In the **Connections** area, configure the connection.  
  
    Type  
    Select **Relational** or **Loader**.  
  
    Value  
    Provide an appropriate connection name.  
  
    Connections  
    Select **Microsoft SQL Server PDW**.  
  
![APS Informatica Session Target dialog box](../sqlpdw/media/APS_Informatica_Session_Target.png "APS_Informatica_Session_Target")  
  
### Setting Relational Connection  
Additional information about relational connection session level properties.  
  
In the **Connections** area, in the **Type** box, select **Relational**.  
  
### Loader connection sessions  
Additional information about loader connection session level properties.  
  
In the **Properties** area, for the **Output file directory** and **Output filename** attributes, enter a directory and filename.  
  
This file will be automatically deleted after the execution of the workflow on both success and failure.  
  
**Output file directory**  
The output file directory specifies the location of the intermediate files used during the bulk loading of the data. The source data is written to a flat file in string format and then loader component loads data from these files to the SQL Server PDW. The data from multiple files is loaded in parallel to boost performance. The default directory is Informatica's target file directory.  
  
**Output filename**  
Name of the output file. The default value is the name of the target table. During runtime, each new file name is appended with a number.  
  
**Load mode**  
Indicates how data is handled during the load process. Options for mode are **append**, **fastappend**, **upsert**, or **reload**. The default mode is **append**.  
  
-   In **append** mode, data is inserted at the end of existing table data.  
  
-   In **fastappend** mode, the data is inserted at the end of the existing table. In addition, **fastappend** loads directly into the final destination table and does not use a staging table. **fastappend** requires multi-transaction mode **(-m)**.  
  
-   In **upsert** mode, if the row does not already exist in the table (based on a given value) the row will be inserted. If the row does already exist in the table, the existing entry will be updated.  
  
-   In **reload** mode, the loader component truncates the destination table, then inserts into the destination table with data from the staging table.  
  
**Use multiple transactions**  
Specifies that multiple transactions can be used inside the engine while loading the data. By default, this is not enabled and the load will be performed as a single transaction (that may require more time, but can be rolled back). Setting this option enables loads to go faster, but is not transactionally safe. This option should only be used for initial loads of tables where performance is a major concern. In the event of a load failure during initial table loads, the destination tables can be manually dropped by the user. Multi-transaction load is only permitted if the destination table is a distributed table; if **-m** is used with the destination table being a replicated table, the load is aborted and an error message is returned to the user.  

**Unmapped columns in upsert mode**  
In upsert mode, you can map a source table to a target table without mapping all of the target columns. When you update a row, the adapter does not update the unmapped target columns. When you insert a row, the adapter sets the unmapped target columns to the default value according to the table definition. If the unmapped column does not have a default value, the adapter sets the value to NULL. If the unmapped columns does not have a default value and is NOT NULLABLE, the adapter will reject the inserted row.


  
## .NET Framework Security  
  
## See Also  
[Installing the Informatica Connector &#40;SQL Server PDW&#41;](../sqlpdw/installing-the-informatica-connector-sql-server-pdw.md)  
[Using SQL Server PDW tables as Informatica Sources and Targets &#40;SQL Server PDW&#41;](../sqlpdw/using-sql-server-pdw-tables-as-informatica-sources-and-targets-sql-server-pdw.md)  
[Using Informatica to create SQL Server PDW connections in workflow manager &#40;SQL Server PDW&#41;](../sqlpdw/using-informatica-to-create-sql-server-pdw-connections-in-workflow-manager-sql-server-pdw.md)  
  
