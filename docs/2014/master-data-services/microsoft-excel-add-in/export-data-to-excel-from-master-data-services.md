---
title: "Load Data from MDS into Excel | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: dd29389b-928c-4e50-995c-c6af27f97805
author: leolimsft
ms.author: lle
manager: craigg
---
# Load Data from MDS into Excel
  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you must load data from the MDS repository in order to work with it.  
  
 If you want to filter the dataset before loading, see [Filter Data before Loading &#40;MDS Add-in for Excel&#41;](filter-data-before-exporting-mds-add-in-for-excel.md) instead.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
### To load data from MDS into Excel  
  
1.  Open Excel and on the **Master Data** tab, connect to an MDS repository. For more information, see [Connect to an MDS Repository &#40;MDS Add-in for Excel&#41;](connect-to-an-mds-repository-mds-add-in-for-excel.md).  
  
2.  In the **Master Data Explorer** pane, select a model and version. The list of entities is populated.  
  
    -   If the **Master Data Explorer** pane is not visible, in the **Connect and Load** group, click **Show Explorer**.  
  
    -   If the **Master Data Explorer** pane is disabled, it is because the existing sheet already contains MDS-managed data. To enable the pane, open a new worksheet.  
  
3.  In the **Master Data Explorer** pane, in the list of entities, double-click the entity you want to load.  
  
    > [!NOTE]  
    >  -   Only the first one million members are loaded into Excel. To filter the list before loading, on the ribbon in the **Connect and Load** group, click **Filter**.  
    > -   In columns that are constrained lists (domain-based attributes), only the first 25,000 values are loaded. You can change this number in the MaximumDbaEntitySize property in the excelusersettings.config file located on the computer that Excel is installed on. This file is located in C:\Users\\<user\>\AppData\Local\Microsoft\Microsoft SQL Server\120\MasterDataServices\\.  
  
    > [!NOTE]  
    >  When you load text-delimited data using the Add-in for Microsoft Excel with 32-bit Excel, and the settings for the **Cell Count to Load** and **Cell Count to Publish** properties are both set to the maximum of 1000, an out-of-memory error will occur. You have to use 64-bit Excel to use the maximum settings for **Cell Count to Load** and **Cell Count to Publish**.  
  
## Next Steps  
 [Publish Data from Excel to MDS &#40;MDS Add-in for Excel&#41;](import-data-from-excel-to-master-data-services-mds-add-in-for-excel.md)  
  
## See Also  
 [Loading Data &#40;MDS Add-in for Excel&#41;](overview-exporting-data-to-excel-mds-add-in-for-excel.md)   
 [Filter Dialog Box &#40;MDS Add-in for Excel&#41;](filter-dialog-box-mds-add-in-for-excel.md)   
 [Publishing Data &#40;MDS Add-in for Excel&#41;](overview-importing-data-from-excel-mds-add-in-for-excel.md)  
  
  
