---
title: Overview
description: Learn how to load data from Master Data Services into Excel, and then publish it back to MDS by using the Master Data Services Add-in for Excel.
ms.custom: microsoft-excel-add-in, seo-lt-2019
ms.date: 07/25/2017
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 33d9c8fc-9602-494d-b9ab-8f0f42785974
author: CordeliaGrey
ms.author: jiwang6
---
# Master Data Services Add-in for Microsoft Excel

[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

With the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]&nbsp;[!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you can load filtered lists of data from Master Data Services (MDS) into Excel and then work with it just as you would any other data. When you are done, you can publish the data back to MDS where it's centrally stored. Security level determines which data you can view and update.  
  
If you're an administrator, you can use the [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)] to create entities and attributes, which you can load with data. This process eliminates the need to use other tools to load data into your models.  
  
With the [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you can use Data Quality Services (DQS) to match data before loading it into MDS. This feature helps to prevent duplicate data in MDS.

## Downloads

- [Master Data Services Add-in for Excel for SQL Server 2016 SP2](https://www.microsoft.com/download/details.aspx?id=56838).
- [[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)] for SQL Server 2017](https://go.microsoft.com/fwlink/?linkid=836867).
- [Master Data Services Add-in for Excel for SQL Server 2019 CTP](https://go.microsoft.com/fwlink/?linkid=2086948).

> [!NOTE]
> The Master Data Services Add-in for Excel requires the Office Automation Security to be set to one of the following:
>
> - Level 1 : Macros enabled (default)
> - Level 2 : Use application macro security level

## Terms

When working with the add-in, you might come across the following terms. For more information about these concepts, see [Master Data Services Overview &#40;MDS&#41;](../../master-data-services/master-data-services-overview-mds.md).  

- The *MDS repository* is where all master data is stored. It's a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database configured to store MDS data. To work with data from the repository, you load it into Excel. When you're done working with it, you publish the changes back to the repository. Administrators can add new entities and attributes to the repository.
  
- *MDS-managed data* is data stored in the MDS repository. When you load MDS-managed data into Excel, it's displayed as highlighted rows. You can also add data to your Excel worksheet that's not MDS-managed. Such data won't be affected if you refresh the MDS-managed data.

- A *model* is a data container. You can create versions of these containers. The latest version is usually the most recent. For more information, see [Models &#40;Master Data Services&#41;](../../master-data-services/models-master-data-services.md).  
  
- An *entity* is a list of data, like a table in a database. For example, the **Color** entity might contain a list of colors. For more information, see [Entities &#40;Master Data Services&#41;](../../master-data-services/entities-master-data-services.md).  
  
- A *member* is a record or a row of data. Each entity contains members. For example, **Blue** could be a member of the **Color** entity. For more information, see [Members &#40;Master Data Services&#41;](../../master-data-services/members-master-data-services.md).  
  
- An *attribute* is a column of data. Each member has attributes. For example, the **Code** attribute for the **Blue** member is **B**. For more information about attributes, see [Attributes &#40;Master Data Services&#41;](../../master-data-services/attributes-master-data-services.md).  
  
## Related tasks  

| Task Description | Topic |
| ---------------------- | ----------- |
| Create a connection to a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] repository. | [Connect to an MDS repository](../../master-data-services/microsoft-excel-add-in/connect-to-an-mds-repository-mds-add-in-for-excel.md) |  
| Load MDS-managed data into Excel. | [Export data to Excel from Master Data Services](../../master-data-services/microsoft-excel-add-in/export-data-to-excel-from-master-data-services.md) |  
| Save a shortcut query to open the currently displayed MDS-managed data in the future. | [Save a shortcut query file](../../master-data-services/microsoft-excel-add-in/save-a-shortcut-query-file-mds-add-in-for-excel.md)|  
| Share shortcuts with others. | [Email a shortcut query file](../../master-data-services/microsoft-excel-add-in/email-a-shortcut-query-file-mds-add-in-for-excel.md) |  
| View all the changes that have been made to a member. | [View all annotations or transactions for a member](../../master-data-services/microsoft-excel-add-in/view-all-annotations-or-transactions-for-a-member-mds-add-in-for-excel.md)|  
| Find duplications before publishing new data. | [Match similar data](../../master-data-services/microsoft-excel-add-in/match-similar-data-mds-add-in-for-excel.md) |  
| Publish data from a worksheet into the MDS repository. | [Import data from Excel to Master Data Services](../../master-data-services/microsoft-excel-add-in/import-data-from-excel-to-master-data-services-mds-add-in-for-excel.md) |  
| Create a new entity by using data in the worksheet. (Administrators only) | [Create an entity](../../master-data-services/microsoft-excel-add-in/create-an-entity-mds-add-in-for-excel.md) |  
| Create a domain-based attribute or a constrained list. (Administrators only) | [Create a domain-based attribute](../../master-data-services/microsoft-excel-add-in/create-a-domain-based-attribute-mds-add-in-for-excel.md) |  
| Set properties for loading and publishing data. (Administrators only) | [Setting properties](../../master-data-services/microsoft-excel-add-in/setting-properties-for-master-data-services-add-in-for-excel.md) |  
  
## Related content  
  
- [Connections &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/connections-mds-add-in-for-excel.md)  
- [Overview: Exporting data to Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-exporting-data-to-excel-mds-add-in-for-excel.md)  
- [Shortcut query files &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/shortcut-query-files-mds-add-in-for-excel.md)  
- [Refreshing data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/refreshing-data-mds-add-in-for-excel.md)  
- [Overview: Importing data from Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-importing-data-from-excel-mds-add-in-for-excel.md)  
- [Validating data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/validating-data-mds-add-in-for-excel.md)  
- [Data quality matching in the MDS Add-in for Excel](../../master-data-services/microsoft-excel-add-in/data-quality-matching-in-the-mds-add-in-for-excel.md)  
- [Building a model &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/building-a-model-mds-add-in-for-excel.md)
- [Setting properties for Master Data Services Add-in for Excel](../../master-data-services/microsoft-excel-add-in/setting-properties-for-master-data-services-add-in-for-excel.md)  
- [Security &#40;Master Data Services&#41;](../../master-data-services/security-master-data-services.md)  
