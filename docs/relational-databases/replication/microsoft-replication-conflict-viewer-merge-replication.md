---
title: "Microsoft Replication Conflict Viewer (Merge Replication) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.replconflictviewer.cvmerge.f1"
ms.assetid: bfef5e21-ac04-4bc5-a55e-595421e34923
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Microsoft Replication Conflict Viewer (Merge Replication)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The Replication Conflict Viewer allows you to view any conflicts that have occurred during replication synchronization. Conflicts occur when the same data is modified at two separate servers, for example, at a Publisher and Subscriber, or at two different Subscribers. Replication automatically resolves conflicts using the conflict resolver you selected when the article was created. However, the Replication Conflict Viewer allows you to choose a different resolution for the conflict when necessary. The following conflicts can occur:  
  
-   Update conflicts. Update conflicts occur when the same data is changed at two locations. One change wins, and the other one loses. You have the option to keep the existing data (the data that won), overwrite the existing data with the data that conflicted with it (the losing data), or merge the winning and losing data and update the existing data.  
  
-   Insert conflicts. Insert conflicts occur when a row is inserted at one location that violates some data consistency rule when merged with changes at other locations. You have the option to keep the existing data (the data that won), overwrite the existing data with the data that conflicted with it (the losing data), or merge the winning and losing data and update the existing data.  
  
-   Delete conflicts. This conflict occurs when the same row is deleted at one location and changed at the other.  
  
 When conflicts are resolved during synchronization, the data from the losing row is written to a conflict table. Whether you accept the original resolution or choose a different resolution for the conflict, the logged conflict row is deleted from the conflict table. You should periodically review conflicts to help reduce the size of the conflict tracking tables.  
  
> [!NOTE]  
>  Conflicts that involve logical records are not displayed in Conflict Viewer. To view information about these conflicts, use replication stored procedures. For more information, see [View Conflict Information for Merge Publications &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/view-conflict-information-for-merge-publications.md).  
  
## Options  
 The Replication Conflict Viewer is divided into two sections. The upper section of the dialog box shows the conflict list for the selected table. When you click an item in the conflict list, the details of the conflict are displayed in the lower section of the dialog box.  
  
 Information describing why the conflict occurred (for example, the same row was updated at both the Publisher and the Subscriber) is displayed in the lower section of the dialog box. The conflict data in the lower section is displayed in two corresponding columns (**Conflict Winner** and **Conflict Loser**). If the conflict is between updated and deleted data, there may be no data to show for the deleted side of the conflict. In this case, the Replication Conflict Viewer displays a message in one of the columns, indicating that the row was deleted at one location and updated at another. It also indicates the suggested resolution.  
  
 Data that cannot be edited in the Replication Conflict Viewer (for example, **rowguid** data) is displayed read-only with the box shaded.  
  
 **Database**  
 Choose a database that includes publications with conflicts.  
  
 **Publication**  
 Choose a publication that includes tables with conflicts.  
  
 **Table**  
 Choose a table that includes conflicts.  
  
 **Define Filter**  
 Click to open the **Define Filters** dialog box.  
  
 **Apply or Remove Filter**  
 Click to apply or remove a filter that has been defined in the **Define Filters** dialog box.  
  
 **Select All**  
 Click to select all conflicts listed in the grid.  
  
 **Select None**  
 Click to deselect all conflicts listed in the grid.  
  
 **Remove**  
 Click to remove selected conflicts from the viewer and their associated metadata from the replication system tables. Equivalent to clicking the Submit Winner button (without making any changes to the data) for each selected conflict.  
  
 **Show all columns**  
 Select to show all columns of the table.  
  
 **Show first five columns and other columns with conflicting data**  
 Select to display the first five columns and any columns that have conflicts. This is helpful when the table has a large number of columns, but you want to see only the columns most relevant to resolving the conflict. The first five columns are always included in this view, as fields that identify a row, such as the primary key or name fields, are often among the first columns of the table.  
  
 **Display Column Information** (**…**)  
 Click to view column information: **Table Name**, **Column Name**, **Data Type**, and **Column Value**. **Column Value** is editable unless the value is displayed as read-only.  
  
 **Submit Winner**  
 Click to keep the row the conflict resolver determined to be the winner. The value of any column that is not displayed as read-only can be changed prior to clicking this button.  
  
 **Submit Loser**  
 Click to accept the row the conflict resolver determined to be the loser. The value of any column that is not displayed as read-only can be changed prior to clicking this button.  
  
 **Log the details of the conflict**  
 Check this box to log the details of the conflict to a file. To specify a location for the file, point to the **View** menu and click **Options**. Enter a value, or click the browse (**...**) and navigate to the appropriate file. Click **OK** to exit the **Options** dialog box.  
  
## See Also  
 [View and Resolve Data Conflicts for Merge Publications &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/view-and-resolve-data-conflicts-for-merge-publications.md)   
 [Advanced Merge Replication Conflict Detection and Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)  
  
  
