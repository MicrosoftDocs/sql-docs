---
title: "Import Data (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 6617b2a2-9f69-433e-89e0-4c5dc92982cf
author: minewiskan
ms.author: owend
manager: craigg
---
# Import Data (SSAS Tabular)
  You can import data into a tabular model from a wide variety of sources. The topics in this section describe how to use the Data Import Wizard to connect to and select data to be imported into a model project.  
  
> [!IMPORTANT]  
>  If any of the tables in your model will contain a large number of rows, consider importing only a subset of the data during model authoring. By importing a subset of the data, you can reduce processing time and consumption of workspace database server resources.  
  
 Using the Table Import Wizard, you can import data from the following data sources:  
  
|**Data Source**|**Description**|  
|---------------------|---------------------|  
|**Relational Databases**|Relational Data Sources include:<br /><br /> Microsoft SQL Server<br /><br /> Microsoft SQL Azure<br /><br /> Microsoft SQL Server Parallel Data Warehouse<br /><br /> Microsoft Access<br /><br /> Oracle<br /><br /> Teradata<br /><br /> Sybase<br /><br /> Informix<br /><br /> IBM DB2<br /><br /> OLEDB/ODBC|  
|**Multidimensional Sources**|Microsoft SQL Server Analysis Services cube|  
|**Data Feeds**|Data feeds include:<br /><br /> Microsoft Reporting Services Report<br /><br /> Azure DataMarket Dataset<br /><br /> Atom feeds from a public or corporate provider|  
|**Text Files**|Text files include:<br /><br /> Excel files (.xlsx)<br /><br /> Text file (.txt)|  
  
 In addition to importing data by using the Table Import Wizard, you can paste copied data (from Clipboard) into a table. Pasted data behaves differently from data that has been imported from other data sources. Pasted data in tables do not have a Connection Name or Source Data property. Pasted data is persisted in the Model.bim file. When the project or Model.bim file is saved, the pasted data is also saved.  
  
## Related Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Import from a Relational Data Source &#40;SSAS Tabular&#41;](import-from-a-relational-data-source-ssas-tabular.md)|Describes how to import data from relational data sources such as a Microsoft SQL Server, Oracle, or Teradata database.|  
|[Import from a Multidimensional Data Source &#40;SSAS Tabular&#41;](import-from-a-multidimensional-data-source-ssas-tabular.md)|Describes how to import data from a multidimensional SQL Server Analysis Services cube.|  
|[Import from a Data Feed &#40;SSAS Tabular&#41;](import-from-a-data-feed-ssas-tabular.md)|Describes how to import data from a data feed such as a Microsoft Reporting Services report or Azure Data Market Dataset.|  
|[Import from a Text File &#40;SSAS Tabular&#41;](import-from-a-text-file-ssas-tabular.md)|Describes how to import data from a Microsoft Excel Workbook or text file.|  
|[Copy and Paste Data &#40;SSAS Tabular&#41;](copy-and-paste-data-ssas-tabular.md)|Describes how to add data to an existing table in the model designer by using Paste and Paste Append.|  
  
  
