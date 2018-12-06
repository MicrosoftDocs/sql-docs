---
title: "Tutorial: Using the OData Source [SSIS] | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 2c64cf8b-5edb-48df-8ffe-697096258f71
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Tutorial: Using the OData Source [SSIS]
  This tutorial walks you through the process to extract the **Employees** collection from the sample **Northwind** OData service (http://services.odata.org/V3/Northwind/Northwind.svc/), and then load it into a flat file.  
  
## 1. Create an Integration Services Project  
  
1.  Launch **SQL Server Data Tools** or [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
2.  Click **File**, point to **New**, and click **Project**.  
  
3.  In the **New Project** dialog box, expand **Installed**, expand **Templates**, expand **Business Intelligence**, and click **Integration Services**.  
  
4.  Select **Integration Services Project** for the type of project.  
  
5.  Enter a **name** and select a **location** for the project, and click **OK**.  
  
## 2. Add and Configure OData Source to the SSIS Package  
  
1.  Drag-drop a **Data Flow Task** from the **SSIS Toolbox** on to the control flow design surface of your SSIS package.  
  
2.  Click the **Data Flow** tab, or double click on the newly added **Data Flow Task** to launch the **Data Flow design surface**.  
  
3.  Drag-drop **OData Source** from the **Common** group in the **SSIS Toolbox**. When the **OData Source** is first installed, it will appear under the **Common** group in the **SSIS Toolbox**.  
  
4.  Double click the **OData Source** component to launch the **OData Source Editor** dialog box.  
  
5.  Click **New...** to add a new OData Connection Manager.  
  
6.  Enter the OData service URL for **Service document location**. This can be the URL to the service document, or to a specific feed or entity. For the purpose of this tutorial, type [http://services.odata.org/V3/Northwind/Northwind.svc/](http://services.odata.org/V3/Northwind/Northwind.svc/).  
  
7.  Confirm that **Windows Authentication** is selected for the **authentication** to use to access the OData Service. **Windows Authentication** is selected by default. To use basic authentication, select **Use this user name and password**.  
  
8.  Click **Test Connection** to the connection, and click **OK** to create an instance of OData Connection Manager.  
  
9. In the **OData Source Editor** Dialog Box, confirm that **Collection** is selected for **Use collection on resource path** option.  
  
10. From the **Collection** drop down list, select **Employees**.  
  
11. Enter any additional OData query options or filters for **Query Options**. Ex. $orderby=CompanyName&$top=100. For the purpose of this tutorial, enter **$top=5**.  
  
12. Click **Preview** to preview the data.  
  
13. Click **Columns** in the left navigation pane to switch to the **Columns** page.  
  
14. Select **EmployeeID**, **FirstName**, and **LastName** from **Available External Columns** by checking the check boxes.  
  
15. Click **OK** to close the **OData Source Editor** dialog box.  
  
## 3. Add Flat File Destination and Test the Solution  
  
1.  Now, drag-drop a **Flat File Destination** from **SSIS Toolbox** to the Data Flow design surface below the **OData Source** component.  
  
2.  Connect **OData Source** component with the **Flat File Destination** component using blue arrow.  
  
3.  Double-click on **Flat File Destination**. You should see the **Flat File Destination Editor** dialog box.  
  
4.  In the **Flat File Destination Editor** dialog box, click **New** to create a new flat file connection manager.  
  
5.  In the **Flat File Format** dialog box, select **Delimited**. You should see the **Flat File Connection Manager Editor** dialog box.  
  
6.  In the **Flat File Connection Manager Editor** dialog box, for the **File name**, enter **c:\Employees.txt**.  
  
7.  In the left navigation pane, click **Columns**. You can preview the data on this page.  
  
8.  Click OK to close the **Flat File Connection Manager** Editor dialog box.  
  
9. In the **Flat File Destination Editor** dialog box, click **Mappings** in the left navigation pane. Review the mappings.  
  
10. Click OK to close the **Flat File Destination Editor** dialog box.  
  
11. Compile and execute the SSIS package. Verify that the output file is created with ID, First Name, and Last Name for 5 employees from the OData feed.  
  
  
