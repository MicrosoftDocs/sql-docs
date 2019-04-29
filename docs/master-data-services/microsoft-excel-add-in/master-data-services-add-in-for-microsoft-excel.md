---
title: "Master Data Services Add-in for Microsoft Excel | Microsoft Docs"
ms.custom: microsoft-excel-add-in
ms.date: "07/25/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 33d9c8fc-9602-494d-b9ab-8f0f42785974
author: leolimsft
ms.author: lle
manager: craigg
---
# Master Data Services Add-in for Microsoft Excel

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  With the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you can load filtered lists of data from MDS into Excel, where you can work with it just as you would any other data. When you are done, you can publish the data back to MDS, where it is centrally stored. Security determines which data you can view and update.  
  
 If you are an administrator, use the [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)] to create entities and attributes and to load them with data. This eliminates the need to use any other tools to load data into your models.  
  
 In the [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you can use Data Quality Services (DQS) to match data before loading it into MDS. This helps to prevent duplicate data in MDS.  

## Downloads 
>*  Download the Master Data Services Add-in for Excel for SQL Server 2016 SP2 from [this Microsoft Download Center page](https://www.microsoft.com/download/details.aspx?id=56838). 
>* Download the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)] for SQL Server 2017 from [this Microsoft Download Center page](https://go.microsoft.com/fwlink/?linkid=836867).
>*  Download the Master Data Services Add-in for Excel for SQL Server 2019 CTP from [this Microsoft Download Center page](https://go.microsoft.com/fwlink/?linkid=2086948). 
 
  
## Terms  
 When working with the Add-in, you may encounter the following terms. For more information about these concepts, see [Master Data Services Overview &#40;MDS&#41;](../../master-data-services/master-data-services-overview-mds.md).  
  
-   The *MDS repository* is where all master data is stored. It is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that is configured to store MDS data. To work with data from the repository, you load data it into Excel; when you're done working with it, you publish changes back to the repository. Administrators can add new entities and attributes to the repository.  
  
-   *MDS-managed data* is data that is stored in the MDS repository and that you load into Excel, where the data is displayed as highlighted rows. You can add data that is not MDS-managed to your worksheet, and it is not affected when you refresh the MDS-managed data.  
  
-   A *model* is a container of data. Versions of these containers can be created, and usually the latest version is the most recent. For more information, see [Models &#40;Master Data Services&#41;](../../master-data-services/models-master-data-services.md).  
  
-   An *entity* is a list of data. You might think of an entity as a table in a database. For example, the **Color** entity might contain a list of colors. For more information, see [Entities &#40;Master Data Services&#41;](../../master-data-services/entities-master-data-services.md).  
  
-   A *member* is a row of data (a record). Each entity contains members. An example of a member is **Blue**. For more information, see [Members &#40;Master Data Services&#41;](../../master-data-services/members-master-data-services.md).  
  
-   An *attribute* is a column of data. Each member has attributes. For example, the **Code** attribute for the **Blue** member is **B**. For more information about attributes, see [Attributes &#40;Master Data Services&#41;](../../master-data-services/attributes-master-data-services.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a connection to a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] repository.|[Connect to an MDS Repository &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/connect-to-an-mds-repository-mds-add-in-for-excel.md)|  
|Load MDS-managed data into Excel.|[Export Data to Excel from Master Data Services](../../master-data-services/microsoft-excel-add-in/export-data-to-excel-from-master-data-services.md)|  
|Save a shortcut query that you can use open the currently displayed MDS-managed data in the future.|[Save a Shortcut Query File &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/save-a-shortcut-query-file-mds-add-in-for-excel.md)|  
|Share shortcuts with others.|[Email a Shortcut Query File &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/email-a-shortcut-query-file-mds-add-in-for-excel.md)|  
|View all changes that have been made to a member.|[View All Annotations or Transactions for a Member &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/view-all-annotations-or-transactions-for-a-member-mds-add-in-for-excel.md)|  
|Before publishing new data, find out whether duplication exists.|[Match Similar Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/match-similar-data-mds-add-in-for-excel.md)|  
|Publish data from a worksheet into the MDS repository.|[Import Data from Excel to Master Data Services &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/import-data-from-excel-to-master-data-services-mds-add-in-for-excel.md)|  
|Create a new entity with data in the worksheet. (Administrators only)|[Create an Entity &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/create-an-entity-mds-add-in-for-excel.md)|  
|Create a domain-based attribute, also known as a constrained list. (Administrators only)|[Create a Domain-based Attribute &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/create-a-domain-based-attribute-mds-add-in-for-excel.md)|  
|Set properties for loading and publishing data in the Master Data Services Add-in for Excel. (Administrators only)|[Setting Properties for Master Data Services Add-in for Excel](../../master-data-services/microsoft-excel-add-in/setting-properties-for-master-data-services-add-in-for-excel.md)|  
  
## Related Content  
  
-   [Connections &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/connections-mds-add-in-for-excel.md)  
  
-   [Overview: Exporting Data to Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-exporting-data-to-excel-mds-add-in-for-excel.md)  
  
-   [Shortcut Query Files &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/shortcut-query-files-mds-add-in-for-excel.md)  
  
-   [Refreshing Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/refreshing-data-mds-add-in-for-excel.md)  
  
-   [Overview: Importing Data from Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-importing-data-from-excel-mds-add-in-for-excel.md)  
  
-   [Validating Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/validating-data-mds-add-in-for-excel.md)  
  
-   [Data Quality Matching in the MDS Add-in for Excel](../../master-data-services/microsoft-excel-add-in/data-quality-matching-in-the-mds-add-in-for-excel.md)  
  
-   [Building a Model &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/building-a-model-mds-add-in-for-excel.md)  
  
-   [Setting Properties for Master Data Services Add-in for Excel](../../master-data-services/microsoft-excel-add-in/setting-properties-for-master-data-services-add-in-for-excel.md)  
  
-   [Security &#40;Master Data Services&#41;](../../master-data-services/security-master-data-services.md)  
  
  
