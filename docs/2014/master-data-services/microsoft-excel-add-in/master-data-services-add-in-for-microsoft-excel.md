---
title: "Master Data Services Add-in for Microsoft Excel | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 33d9c8fc-9602-494d-b9ab-8f0f42785974
author: leolimsft
ms.author: lle
manager: craigg
---
# Master Data Services Add-in for Microsoft Excel
  With the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], master lists of reference data can be distributed to everyone at your organization who uses Excel. Security determines which data users can view and update.  
  
 You can load filtered lists of data from MDS into Excel, where you can work with it just as you would any other data. When you are done, you can publish the data back to MDS, where it is centrally stored.  
  
 If you are an administrator, use the [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)] to create entities and attributes and to load them with data. This eliminates the need to use any other tools to load data into your models.  
  
 In the [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you can use Data Quality Services (DQS) to match data before loading it into MDS. This helps to prevent duplicate data in MDS.  
  
> [!IMPORTANT]  
>  You can continue using the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 version of Master Data Services Add-In for Excel after upgrading Master Data Services and Data Quality Services to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CTP2. However, any earlier version of the Master Data Services Add-In for Excel will not work after upgrading to SQL Server 2014 CTP2. You can download the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 version of Master Data Services Add-In for Excel from [here](https://go.microsoft.com/fwlink/?LinkId=328664).  
  
## Terms  
 When working with the Add-in, you may encounter the following terms.  
  
-   The *MDS repository* is where all master data is stored. It is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that is configured to store MDS data. To work with data from the repository, you load data it into Excel; when you're done working with it, you publish changes back to the repository. Administrators can add new entities and attributes to the repository.  
  
-   *MDS-managed data* is data that is stored in the MDS repository and that you load into Excel, where the data is displayed as highlighted rows. You can add data that is not MDS-managed to your worksheet, and it is not affected when you refresh the MDS-managed data.  
  
-   A *model* is a container of data. Versions of these containers can be created, and usually the latest version is the most recent. For more information, see [Models &#40;Master Data Services&#41;](../models-master-data-services.md).  
  
-   An *entity* is a list of data. You might think of an entity as a table in a database. For example, the **Color** entity might contain a list of colors. For more information, see [Entities &#40;Master Data Services&#41;](../entities-master-data-services.md).  
  
-   A *member* is a row of data. Each entity contains members. An example of a member is **Blue**. For more information, see [Members &#40;Master Data Services&#41;](../members-master-data-services.md).  
  
-   An *attribute* is a column of data. Each member has attributes. For example, the **Code** attribute for the **Blue** member is **B**. For more information about attributes, see [Attributes &#40;Master Data Services&#41;](../attributes-master-data-services.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a connection to a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] repository.|[Connect to an MDS Repository &#40;MDS Add-in for Excel&#41;](connect-to-an-mds-repository-mds-add-in-for-excel.md)|  
|Load MDS-managed data into Excel.|[Load Data from MDS into Excel](export-data-to-excel-from-master-data-services.md)|  
|Save a shortcut query that you can use open the currently displayed MDS-managed data in the future.|[Save a Shortcut Query File &#40;MDS Add-in for Excel&#41;](save-a-shortcut-query-file-mds-add-in-for-excel.md)|  
|Share shortcuts with others.|[Email a Shortcut Query File &#40;MDS Add-in for Excel&#41;](email-a-shortcut-query-file-mds-add-in-for-excel.md)|  
|View all changes that have been made to a member.|[View All Annotations or Transactions for a Member &#40;MDS Add-in for Excel&#41;](view-all-annotations-or-transactions-for-a-member-mds-add-in-for-excel.md)|  
|Before publishing new data, find out whether duplication exists.|[Match Similar Data &#40;MDS Add-in for Excel&#41;](match-similar-data-mds-add-in-for-excel.md)|  
|Publish data from a worksheet into the MDS repository.|[Publish Data from Excel to MDS &#40;MDS Add-in for Excel&#41;](import-data-from-excel-to-master-data-services-mds-add-in-for-excel.md)|  
|Create a new entity with data in the worksheet. (Administrators only)|[Create an Entity &#40;MDS Add-in for Excel&#41;](create-an-entity-mds-add-in-for-excel.md)|  
|Create a domain-based attribute, also known as a constrained list. (Administrators only)|[Create a Domain-based Attribute &#40;MDS Add-in for Excel&#41;](create-a-domain-based-attribute-mds-add-in-for-excel.md)|  
|Set properties for loading and publishing data in the Master Data Services Add-in for Excel. (Administrators only)|[Setting Properties for Master Data Services Add-in for Excel](setting-properties-for-master-data-services-add-in-for-excel.md)|  
  
## Related Content  
  
-   [Connections &#40;MDS Add-in for Excel&#41;](connections-mds-add-in-for-excel.md)  
  
-   [Loading Data &#40;MDS Add-in for Excel&#41;](overview-exporting-data-to-excel-mds-add-in-for-excel.md)  
  
-   [Shortcut Query Files &#40;MDS Add-in for Excel&#41;](shortcut-query-files-mds-add-in-for-excel.md)  
  
-   [Data Quality Matching in the MDS Add-in for Excel](data-quality-matching-in-the-mds-add-in-for-excel.md)  
  
-   [Publishing Data &#40;MDS Add-in for Excel&#41;](overview-importing-data-from-excel-mds-add-in-for-excel.md)  
  
-   [Building a Model &#40;MDS Add-in for Excel&#41;](building-a-model-mds-add-in-for-excel.md)  
  
-   [Security &#40;Master Data Services&#41;](../security-master-data-services.md)  
  
  
