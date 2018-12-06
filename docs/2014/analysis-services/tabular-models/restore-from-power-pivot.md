---
title: "Restore from PowerPivot | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql11.asvs.ssmsimbi.RestoreFromPP.f1"
ms.assetid: 232ac8ed-77fe-47d8-acd3-59bc2fdfdf48
author: minewiskan
ms.author: owend
manager: craigg
---
# Restore from PowerPivot
  You can use the Restore from PowerPivot feature in SQL Server Management Studio to create a new Tabular model database on an Analysis Services instance (running in Tabular mode), or restore to an existing database from a PowerPivot workbook (.xlsx).  
  
> [!NOTE]  
>  The Import from PowerPivot project template in SQL Server Data Tools provides similar functionality. For more information, see [Import from PowerPivot &#40;SSAS Tabular&#41;](import-from-power-pivot-ssas-tabular.md).  
  
 When using Restore from PowerPivot, keep the following in mind:  
  
-   In order to use Restore from PowerPivot, you must be logged on as a member of the Server Administrators role on the Analysis Services instance.  
  
-   The Analysis Services instance service account must have Read permissions on the workbook file you are restoring from.  
  
-   By default, when you restore a database from PowerPivot, the Tabular model database Data Source Impersonation Info property is set to Default, which specifies the Analysis Services instance service account. It is recommended you change the impersonation credentials to a Windows user account in Database Properties. For more information, see [Impersonation &#40;SSAS Tabular&#41;](impersonation-ssas-tabular.md).  
  
-   Data in the PowerPivot data model will be copied into an existing or new Tabular model database on the Analysis Services instance. If your PowerPivot workbook contains linked tables, they will be recreated as a table without a data source, similar to a table created using Paste To New table.  
  
### To Restore from PowerPivot  
  
1.  In SSMS, in the Active Directory instance you want to restore to, right click **Databases**, and then click **Restore from PowerPivot**.  
  
2.  In the **Restore from PowerPivot** dialog box, in **Restore Source**, in **Backup file**, click **Browse**, and then select an .abf or .xslx file to restore from.  
  
3.  In **Restore Target**, in **Restore database**, type a name for a new database or for an existing database. If you do not specify a name, the name of the workbook is used.  
  
4.  In **Storage location**, click **Browse**, and then select a location to store the database.  
  
5.  In **Options**, leave **Include security information** checked. When restoring from a PowerPivot workbook, this setting does not apply.  
  
## See Also  
 [Tabular Model Databases &#40;SSAS Tabular&#41;](tabular-model-databases-ssas-tabular.md)   
 [Import from PowerPivot &#40;SSAS Tabular&#41;](import-from-power-pivot-ssas-tabular.md)  
  
  
