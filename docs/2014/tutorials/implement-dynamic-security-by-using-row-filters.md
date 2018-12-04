---
title: "Implement Dynamic Security by Using Row Filters | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 8bf03c45-caf5-4eda-9314-e4f8f24a159f
author: minewiskan
ms.author: owend
manager: craigg
---
# Implement Dynamic Security by Using Row Filters
  In this supplemental lesson, you will create an additional role that implements dynamic security. Dynamic security provides row-level security based on the user name or login id of the user currently logged on. To learn more, see [Roles &#40;SSAS Tabular&#41;](../analysis-services/tabular-models/roles-ssas-tabular.md).  
  
 To implement dynamic security, you must add a table to your model containing the Windows user names of those users that can create a connection to the model as a data source and browse model objects and data. The model you create using this tutorial is in the context of Adventure Works Corp.; however, in order to complete this lesson, you must add a table containing users from your own domain. You will not need the passwords for the user names that will be added. To create an Employee Security table, with a small sample of users from your own domain, you will use the Paste feature, pasting employee data from an Excel spreadsheet. In a real-world scenario, the table containing user names you add to a model would typically use a table from an actual database as a data source; for example, a real dimEmployee table.  
  
 In order to implement dynamic security, you will use two new DAX functions: [USERNAME Function &#40;DAX&#41;](https://msdn.microsoft.com/library/hh230954.aspx) and [LOOKUPVALUE Function &#40;DAX&#41;](https://msdn.microsoft.com/library/gg492170.aspx). These functions, applied in a row filter formula, are defined in a new role. Using the LOOKUPVALUE function, the formula specifies a value from the Employee Security table and then passes that value to the USERNAME function, which specifies the user name of the user logged on belongs to this role. The user can then browse only data specified by the role's row filters. In this scenario, you will specify that sales employees can only browse internet sales data for the sales territories in which they are a member.  
  
 In order to complete this supplemental lesson, you will complete a series of tasks. Those tasks that are unique to this Adventure Works tabular model scenario, but would not necessarily apply to a real-world scenario are identified as such. Each task includes additional information describing the purpose of the task.  
  
 Estimated time to complete this lesson: **30 minutes**  
  
## Prerequisites  
 This supplemental lesson topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this supplemental lesson, you should have completed all previous lessons.  
  
## Add the dimSalesTerritory table to the AW Internet Sales Tabular Model Project  
 In order to implement dynamic security for this Adventure Works scenario, you must add two additional tables to your model. The first table you will add is dimSalesTerritory (as Sales Territory) from the same AdventureWorksDW database. You will later apply a row filter to the Sales Territory table that defines the particular data the logged on user can browse.  
  
#### To add the dimSalesTerritory table  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click on the **Model** menu, and then click **Existing Connections**.  
  
2.  In the **Existing Connections** dialog box, verify the **Adventure Works DB from SQL** data source connection is selected, and then click **Open**.  
  
     If the Impersonation Credentials dialog box appears, type the impersonation credentials you used in Lesson 2: Add Data.  
  
3.  On the **Choose How to Import the Data** page, leave **Select from a list of tables and views to choose the data to import** selected, and then click **Next**.  
  
4.  On the **Select Tables and Views** page, select the **DimSalesTerritory** table.  
  
5.  In the Friendly Name column, type **Sales Territory**.  
  
6.  Click **Preview and Filter**.  
  
7.  Deselect the **SalesTerritoryAlternateKey** column, and then click **Ok**.  
  
8.  On the **Select Tables and Views** page, click **Finish**.  
  
     The new table will be added to the model workspace. Objects and data from the source dimSalesTerritory table are then imported into the new Sales Territory table in your AW Internet Sales Tabular Model.  
  
9. After the table has been imported, click **Close**.  
  
## Give the Columns Friendly Names  
 In this task, you will rename the columns in the Sales Territory table, giving them friendly names. It is not always necessary to give tables and/or columns friendly names; however, it does make your model project easier to navigate in the model designer as well as for users browsing model objects and data in a client application field list.  
  
#### To rename Columns in the Sales Territory Table  
  
-   In the model designer, rename the columns in the **Sales Territory** table:  
  
     **Sales Territory**  
  
    |Source Name|Friendly Name|  
    |-----------------|-------------------|  
    |SalesTerritoryKey|Sales Territory Id|  
    |SalesTerritoryRegion|Sales Territory Region|  
    |SalesTerritoryCountry|Sales Territory Country|  
    |SalesTerritoryGroup|Sales Territory Group|  
  
## Add a table with user name data  
 Because the dimEmployee table in the AdventureWorksDW sample database contains users from the AdventureWorks domain, and those user names do not exist in your own environment, you must create a table in your model that contains a small sample (three) of actual users from your organization. You will then add these users as members to the new role. You do not need the passwords for the sample user names, but you will need actual Windows user names from your own domain.  
  
#### To add an Employee Security table  
  
1.  Open Microsoft Excel, creating a new worksheet.  
  
2.  Copy the following table, including the header row, and then paste it into the worksheet.  
  
    |Employee Id|Sales Territory Id|First Name|Last Name|Login Id|  
    |-----------------|------------------------|----------------|---------------|--------------|  
    |1|2|\<user first name>|\<user last name>|\<domain\username>|  
    |1|3|\<user first name>|\<user last name>|\<domain\username>|  
    |2|4|\<user first name>|\<user last name>|\<domain\username>|  
    |3|5|\<user first name>|\<user last name>|\<domain\username>|  
  
3.  In the new worksheet, replace the first name, last name, and domain\username with the names and login ids of three users in your organization. Put the same user on the first two rows, for Employee Id 1. This will show this user belongs to more than one sales territory. Leave the Employee Id and Sales Territory Id fields as they are.  
  
4.  Save the worksheet as `Sample Employee`.  
  
5.  In the worksheet, select all of the cells with employee data, including the headers, then right click the selected data, and then click **Copy**.  
  
6.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Edit** menu, and then click **Paste**.  
  
     If Paste is greyed out, click any column in any table in the model designer window, and then click the **Edit** menu, and then click **Paste**.  
  
7.  In the **Paste Preview** dialog box, in **Table Name**, type `Employee Security`.  
  
8.  In **Data to be pasted**, verify the data includes all of the user data and headers from the Sample Employee worksheet.  
  
9. Verify **Use first row as column headers** is checked, and then click **Ok**.  
  
     A new table named Employee Security with employee data copied from the Sample Employee worksheet is created.  
  
## Create Relationships Between Internet Sales, Geography, and Sales Territory table  
 The Internet Sales, Geography, and Sales Territory table all contain a common column, Sales Territory Id. The Sales Territory Id column in the Sales Territory table contains values with a different Id for each sales territory.  
  
#### To create relationships between the Internet Sales, Geography, and the Sales Territory table  
  
1.  In the model designer, in Diagram View, in the **Geography** table, click and hold on the **Sales Territory Id** column, then drag the cursor to the **Sales Territory Id** column in the **Sales Territory** table, and then release.  
  
2.  In the **Internet Sales** table, click and hold on the **Sales Territory Id** column, then drag the cursor to the **Sales Territory Id** column in the **Sales Territory** table, and then release.  
  
     Notice the Active property for this relationship is False, meaning it is inactive. This is because the Internet Sales table already has another active relationship that is used in measures.  
  
## Hide the Employee Security Table from Client Applications  
 In this task, you will hide the Employee Security table, keeping it from appearing in a client application's field list. Keep in-mind that hiding a table does not secure it. Users can still query Employee Security table data if they know how. In order to secure the Employee Security table data, preventing users from being able to query any of its data, you will apply a filter in a later task.  
  
#### To hide the Employee Table from client applications  
  
-   In the model designer, in Diagram View, right-click the **Employee** table heading, and then click **Hide from Client Tools**.  
  
## Create a Sales Employees by Territory user role  
 In this task, you will create a new user role. This role will include a row filter defining which rows of the Sales Territory table are visible to users. The filter is then applied in the one-to many relationship direction to all other tables related to Sales Territory. You will also apply a simple filter that secures the entire Employee Security table from being queryable by any user that is a member of the role.  
  
> [!NOTE]  
>  The Sales Employees by Territory role you create in this lesson restricts members to browse (or query) only sales data for the sales territory to which they belong. If you add a user as a member to the Sales Employees by Territory role that also exists as a member in a role created in [Lesson 12: Create Roles](../analysis-services/lesson-11-create-roles.md), you will get a combination of permissions. When a user is a member of multiple roles, the permissions, and row filters defined for each role are cumulative. That is, the user will have the greater permissions determined by the combination of roles.  
  
#### To create a Sales Employees by Territory user role  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Model** menu, and then click **Roles**.  
  
2.  In the **Role Manager** dialog box, click **New**.  
  
     A new role with the None permission is added to the list.  
  
3.  Click on the new role, and then in the **Name** column, rename the role to `Sales Employees by Territory`.  
  
4.  In the **Permissions** column, click the dropdown list, and then select the **Read** permission.  
  
5.  Click on the **Members** tab, and then click **Add**.  
  
6.  In the **Select User or Group** dialog box, in **Enter the object named to select**, type the first sample user name you used when creating the Employee Security table. Click **Check Names** to verify the user name is valid, and then click **Ok**.  
  
     Repeat this step, adding the other sample user names you used when creating the Employee Security table.  
  
7.  Click on the **Row Filters** tab.  
  
8.  For the `Employee Security` table, in the **DAX Filter** column, type the following formula.  
  
     `=FALSE()`  
  
     When you have finished building the formula, press ENTER.  
  
     This formula specifies that all columns resolve to the false Boolean condition; therefore, no columns for the Employee Security table can be queried by a member of the Sales Employees by Territory user role.  
  
9. For the **Sales Territory** table, type the following formula.  
  
     `='Sales Territory'[Sales Territory Id]=LOOKUPVALUE('Employee Security'[Sales Territory Id], 'Employee Security'[Login Id], USERNAME(), 'Employee Security'[Sales Territory Id], 'Sales Territory'[Sales Territory Id])`  
  
     When you have finished building the formula, press ENTER.  
  
     In this formula, the LOOKUPVALUE function returns all values for the Employee Security[Sales Territory Id] column, where the Employee Security[Login Id] is the same as the current logged on Windows user name, and Employee Security[Sales Territory Id] is the same as the Sales Territory[Sales Territory Id].  
  
     The set of Sales Territory IDs returned by LOOKUPVALUE is then used to restrict the rows shown in the Sales Territory table. Only rows where the Sales Territory ID for the row is in the set of IDs returned by the LOOKUPVALUE function are displayed.  
  
10. In the Role Manager dialog box, click **Ok**.  
  
## Test the Sales Employees by Territory User Role  
 In this task, you will use the Analyze in Excel feature in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] to test the efficacy of the Sales Employees by Territory user role. You will specify one of the user names you added to the Employee Security table and as a member of the role. This user name will then be used as the effective user name in the connection created between Excel and the model.  
  
#### To test the Sales Employees by Territory user role  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Model** menu, and then click **Analyze in Excel**.  
  
2.  In the **Analyze in Excel** dialog box, in **Specify the user name or role to use to connect to the model**, select **Other Windows User**, and then click **Browse**.  
  
3.  In the **Select User or Group** dialog box, in **Enter the object name to select**, type one of the user names you included in the Employee table, and then click **Check Names**.  
  
4.  Click **Ok** to close the **Select User or Group** dialog box, and then click **Ok** to close the **Analyze in Excel** dialog box.  
  
     Excel will open with a new workbook. A Pivot table is automatically created. The Pivot Table Field List includes most of the data fields available in your new model.  
  
     Notice the Employee Security table is not visible in the Pivot Table Field List. This is because you chose to hide this table from client tools in a previous task.  
  
5.  In the **Pivot Table Field** list, in **∑ Internet Sales** (measures), select the **Internet Total Sales** measure. The measure will be entered into the **Values** fields.  
  
6.  In the **Pivot Table Field** list, select the **Sales Territory Id** column from the **Sales Territory** table. The column will be entered into the **Row Labels** fields.  
  
     Notice Internet sales figures appear only for the one region to which the effective user name you used belongs. If you select another column; for example, City, from the Geography table as Row Label field, only cities in the sales territory to which the effective user belongs are displayed.  
  
     This user cannot browse or query any Internet sales data for territories other than the one they belong because the row filter defined for the Sales Territory table in the Sales Employees by Territory user role effectively secures data for all data related to other sales territories.  
  
## See Also  
 [USERNAME Function &#40;DAX&#41;](https://msdn.microsoft.com/library/hh230954.aspx)   
 [LOOKUPVALUE Function &#40;DAX&#41;](https://msdn.microsoft.com/library/gg492170.aspx)   
 [CUSTOMDATA Function &#40;DAX&#41;](https://msdn.microsoft.com/library/hh213140.aspx)  
  
  
