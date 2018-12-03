---
title: "Publishing Data (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: ea84a9aa-aeec-411b-ab8d-bc1b14f864a3
author: leolimsft
ms.author: lle
manager: craigg
---
# Publishing Data (MDS Add-in for Excel)
  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], publish data to the MDS repository when you want to share it with other users. As soon as data is published, it is available for other users of the Add-in to download.  
  
 When you publish data, any data you've added or updated is published to the MDS repository. Data you've deleted is not published-you must delete data separately. For more information, see [Delete a Row &#40;MDS Add-in for Excel&#41;](delete-a-row-mds-add-in-for-excel.md).  
  
> [!NOTE]  
>  Publishing cannot be used to create a new entity. For more information about creating entities, see [Create an Entity &#40;MDS Add-in for Excel&#41;](create-an-entity-mds-add-in-for-excel.md).  
  
## When Multiple Users Publish at the Same Time  
 Multiple users can publish updates to the same data. As each user publishes, the update is saved to the database. This means that a user who was not working with the most recently-updated data can still change the value when he or she publishes.  
  
 Only the updates you make are published to the database. Any out-of-date data in the worksheet is not published.  
  
## Transactions and Annotations  
 Each published change is saved as a transaction. If you choose, you can add annotations (comments) to a transaction, to explain why you made the change.  
  
-   You cannot annotate deletions, although deletions are saved as transactions that can be reversed by an administrator.  
  
-   If you change the **Code** value for a member, it is not recorded as a transaction, and all previous transactions for the member are unavailable.  
  
-   You can view transactions made to a member by other users. You can also view all transactions you've made to a member, even if you no longer have permission to specific attributes.  
  
 You can view all transactions made to a member. For more information, see [View All Annotations or Transactions for a Member &#40;MDS Add-in for Excel&#41;](view-all-annotations-or-transactions-for-a-member-mds-add-in-for-excel.md).  
  
> [!IMPORTANT]  
>  If you enter an annotation of more than 500 characters, the annotation is automatically truncated.  
  
## Business Rule and Other Validation  
 When you publish data, validation is performed to ensure data is accurate before it's added to the MDS repository. If the data does not meet specified criteria, it will not be published to the repository. For more information, see [Validating Data &#40;MDS Add-in for Excel&#41;](validating-data-mds-add-in-for-excel.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Publish data from the active worksheet back to the MDS repository.|[Publish Data from Excel to MDS &#40;MDS Add-in for Excel&#41;](import-data-from-excel-to-master-data-services-mds-add-in-for-excel.md)|  
|Delete a row from the MDS repository and from the worksheet at the same time.|[Delete a Row &#40;MDS Add-in for Excel&#41;](delete-a-row-mds-add-in-for-excel.md)|  
  
## Related Content  
  
-   [Refreshing Data &#40;MDS Add-in for Excel&#41;](refreshing-data-mds-add-in-for-excel.md)  
  
-   [Master Data Services Add-in for Microsoft Excel](master-data-services-add-in-for-microsoft-excel.md)  
  
  
