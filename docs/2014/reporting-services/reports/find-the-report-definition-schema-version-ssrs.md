---
title: "Find the Report Definition Schema Version (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "XML schemas [Reporting Services]"
  - "Report Definition Language, XML schema"
  - "schemas [Reporting Services]"
ms.assetid: 67954419-1b61-4481-a3b9-23b4ba7a5624
author: markingmyname
ms.author: maghan
manager: craigg
---
# Find the Report Definition Schema Version (SSRS)
  A report definition file specifies the RDL namespace for the version of the report definition schema that is used to validate the rdl file. When you open an .rdl file in a report authoring environment such as Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or Report Builder, if the report was created for a previous namespace, a backup file is automatically created, and the report is upgraded to the current namespace. If you save the upgraded report definition, you have saved the converted .rdl file. This is the only way to upgrade a report definition. The report definition itself is not upgraded on a report server. The compiled report is upgraded on a report server. For more information, see [Upgrade Reports](../install-windows/upgrade-reports.md).  
  
### How to: Identify the RDL Schema Version of a Report  
  
1.  Open the report .rdl file in an application such as Notepad or XML Notepad 2007 in which you can view the xml.  
  
     The XML Report element specifies the schema namespace. For example, the following Report element specifies the namespace for Report Designer and the namespace for the report definition.  
  
    ```  
    <Report xmlns:rd=https://schemas.microsoft.com/SQLServer/reporting/reportdesigner   
    xmlns="https://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition">  
    ```  
  
     The report definition namespace is specified by the following URL: `https://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition`.  
  
### How to: Identify the RDL Schema Version of Report Designer  
  
1.  Open a new project. The version of the project that you choose determines the version of the RDL schema. In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], more than one schema version is supported. For more information, see [Deployment and Version Support in SQL Server Data Tools &#40;SSRS&#41;](../tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md).  
  
2.  On the **Project** menu, click **Add New Item**. The **Add New Item** dialog box opens.  
  
3.  In the **Templates** pane, click **Report**.  
  
4.  In **Name**, type a report name or accept the default.  
  
5.  Click **Add**. Report Designer opens a new blank report in Design view.  
  
6.  On the **View** menu, click **Code**. The report definition is displayed as an XML file.  
  
     The XML Report element specifies the schema namespace. For example, the following Report element specifies the namespace for Report Designer and the namespace for the report definition.  
  
    ```  
    <Report xmlns:rd=https://schemas.microsoft.com/SQLServer/reporting/reportdesigner  
    xmlns="https://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition">  
    ```  
  
     The report definition namespace is specified by the following URL: `https://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition`  
  
### How to: Identify the RDL Schema Version on the Report Server  
  
-   In Report Manager, type the URL for the report server. For example, the following URL specifies a report server on the local computer:  
  
     `http://localhost/reportserver/reportdefinition.xsd`  
  
     The .xsd file opens in the browser.  
  
     The XML schema element specifies the schema namespace. For example, the following schema element specifies three namespaces: the targetNamespace reference that is used internally by [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], the xsd reference for the schema itself (xsd), and the report definition reference.  
  
    ```  
    <xsd:schema   
    targetNamespace="https://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition"   
    xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
    xmlns="https://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition"   
    elementFormDefault="qualified">  
    ```  
  
     The report definition namespace is specified by the following URL: `https://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition`  
  
## See Also  
 [Upgrade Reports](../install-windows/upgrade-reports.md)   
 [Report Definition Language &#40;SSRS&#41;](report-definition-language-ssrs.md)  
  
  
