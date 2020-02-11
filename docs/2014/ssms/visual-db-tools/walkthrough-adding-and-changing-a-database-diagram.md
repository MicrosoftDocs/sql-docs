---
title: "Walkthrough: Adding and Changing a Database Diagram | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "database diagrams [SQL Server], about database diagrams"
  - "database diagrams [SQL Server], designing"
  - "database diagrams [SQL Server], creating"
ms.assetid: 228caa0d-8f24-46ab-86d1-b6d8631322bc
author: stevestein
ms.author: sstein
manager: craigg
---
# Walkthrough: Adding and Changing a Database Diagram
  This walkthrough illustrates how to create and modify a database diagram and make changes to the database through the database diagrams component. You will see how to add tables to diagrams, create relationships between tables, create constraints and indexes on columns, and change the level of information you see for each table.  
  
## Prerequisites  
 In order to complete this walkthrough, you will need:  
  
-   Access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database  
  
-   An account with database owner `dbo` privileges  
  
> [!NOTE]  
>  If you attempt to make changes when using an account without sufficient privileges to make changes to tables, then an error message appears.  
  
## Creating a Diagram  
  
#### To create a new database diagram  
  
1.  On the **View** menu, click **Object Explorer**.  
  
2.  Open the Databases node and then open the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] node.  
  
3.  Right-click the Database Diagrams node and choose **New Database Diagram**.  
  
     If the database does not have objects necessary to create diagrams, the following message appears: **This database does not have one or more of the support objects required to use database diagramming. Do you wish to create them?** Choose **Yes**.  
  
     The **Add Table** dialog box appears.  
  
4.  Select **AddressType (Person)** and **Address (Person)** and click **Add**.  
  
     Two tables are added to the diagram.  
  
5.  Close the **Add Table** dialog box.  
  
#### To view different column data  
  
1.  Right-click the `Address` table. On the shortcut menu, point to **Table View**, and then click **Standard**.  
  
     The table grid shows three columns: **Column Name**, **Data Type**, and **Allow Nulls**.  
  
2.  Right-click the `Address` table, click **Table View** and select **Keys**.  
  
     The table grid shows one column, with the table-column names. Only those columns participating in indexes appear.  
  
## Creating New Tables  
  
#### To create tables within Diagram Designer  
  
1.  Right-click the Diagram Designer outside the existing tables and choose **New Table**.  
  
2.  In the **Choose Name** dialog box, click **OK** to accept the default name `Table1`.  
  
     A new table grid appears with three columns: **Column Name**, **Data Type**, and **Allow Nulls**.  
  
3.  Add the following information to `Table1`:  
  
    |**Column Name**|**Data Type**|**Allow Nulls**|  
    |---------------------|-------------------|---------------------|  
    |`T1col1`|`int`|checked|  
    |`T1col2`|`varchar(50)`|checked|  
    |`T1col3`|`float`|checked|  
  
4.  Right-click `T1col1` and select **Set Primary Key**.  
  
     A key icon will appear beside the column name.  
  
5.  From the **File** menu, click **Save Diagram1**.  
  
6.  In the **Choose Name** dialog box, click **OK** to accept the default name `Diagram1`.  
  
7.  The **Save** dialog box appears with a message that `Table1` will be saved to the database. Click **Yes**.  
  
## Modifying Table Structure  
 You can add check constraints and make relationships between tables in Diagram Designer.  
  
#### To create check constraints  
  
1.  In `Table1`, right-click the `T1col3` row and choose **Check Constraints**.  
  
     The **Check Constraints** dialog box appears.  
  
2.  Click **Add**.  
  
     A new constraint appears in the **Selected Check Constraint** list, with the default name `CK_Table1`.  
  
3.  Select the **Expression** row in the grid and click the ellipsis button.  
  
     The **Check Constraint Expression** dialog box appears.  
  
4.  Type `T1col3 > 5` and click **OK**.  
  
     `Table1` now has a constraint that all values entered into `T1col3` must be greater than 5.  
  
5.  Click **Close**.  
  
#### To create relationships between tables  
  
1.  Create a new table in Diagram designer named `Table2` with the following columns:  
  
    |**Column Name**|**Data Type**|**Allow Nulls**|  
    |---------------------|-------------------|---------------------|  
    |`T2col1`|`int`|not checked|  
    |`T2col2`|`varchar(50)`|checked|  
    |`T2col3`|`xml`|checked|  
  
    > [!NOTE]  
    >  The columns on the primary key side of a foreign key relationship must participate in either a Primary Key or a Unique Constraint.  
  
2.  Drag `T2col1` to `T1col1`.  
  
     Two dialog boxes appear: **Foreign Key Relationship** in the background and **Tables and Columns** in the foreground.  
  
3.  Click **OK** to save the new relationship.  
  
4.  Click **OK** again.  
  
## Creating Indexes  
 You can create indexes on most types of data, including XML.  
  
#### To create a standard index  
  
1.  Right-click `Table1` and choose **Indexes/Keys**.  
  
     The **Indexes/Keys** dialog box appears.  
  
2.  Click **Add**.  
  
     A new index appears in the **Selected Primary/Unique Key or Index** list, with a default name similar to `IX_Table1`.  
  
3.  Select the **Columns** row and click the ellipsis button.  
  
     The **Index Columns** dialog box appears.  
  
4.  Click the drop-down arrow under **Column Name** and select `T1col2`.  
  
    > [!NOTE]  
    >  You may add additional columns to this index by selecting the cell below `T1col2` and choosing another column name.  
  
5.  Click **OK** to save this index.  
  
6.  Click **Close** in the **Indexes/Keys** dialog box.  
  
#### To create an XML index  
  
1.  Right-click `T2col1` and choose **Set Primary Key**.  
  
    > [!NOTE]  
    >  Adding an XML index requires that another column in the table be set as a clustered primary key.  
  
2.  Right-click the `T2col3` row in `Table2` and select **XML Indexes**.  
  
     The **XML Indexes** dialog box appears.  
  
3.  Click **Add**.  
  
     An XML index with default values will be added to the **Selected XML Index** list.  
  
4.  Click **Close**.  
  
    > [!NOTE]  
    >  XML indexes are created per-column. The first XML index is primary; any additional indexes are secondary.  
  
## Saving the Diagram  
 All of the changes you make to a diagram are not posted to the database until you save it. If there are problems or conflicts, a dialog box appears with more information.  
  
#### To save a database diagram  
  
1.  On the **File** menu, select **Save Diagram1**.  
  
     The **Save** dialog box appears. If **Warn about Tables Affected** is selected, information about new or changed tables is listed.  
  
2.  Click **OK**.  
  
3.  If any errors occurred, the **Post-Save Notifications** dialog box appears with the errors and their causes. Fix the errors and save the diagram again.  
  
## Next Steps  
 This is a basic diagram with just two existing and two new tables, but it illustrates the potential for diagramming an existing database or creating a new schema visually. Suggestions for more exploration include:  
  
-   Create new diagrams containing groups of related tables  
  
-   Customize the amount of information shown for each table  
  
-   Change the layout and add annotations  
  
-   Copy the diagram to a bitmap  
  
## See Also  
 [Customize the Amount of Information Displayed in Diagrams &#40;Visual Database Tools&#41;](visual-database-tools.md)   
 [Set Up Database Diagram Designer &#40;Visual Database Tools&#41;](set-up-database-diagram-designer-visual-database-tools.md)   
 [Add Tables to Diagrams &#40;Visual Database Tools&#41;](add-tables-to-diagrams-visual-database-tools.md)   
 [Create Relationships Between Tables on a Diagram &#40;Visual Database Tools&#41;](create-relationships-between-tables-on-a-diagram-visual-database-tools.md)   
 [Create XML Indexes](../../relational-databases/xml/create-xml-indexes.md)   
 [Copy an Image of a Database Diagram to the Clipboard &#40;Visual Database Tools&#41;](copy-an-image-of-a-database-diagram-to-the-clipboard-visual-database-tools.md)   
 [Work with Diagram Layout &#40;Visual Database Tools&#41;](work-with-diagram-layout-visual-database-tools.md)  
  
  
