---
description: "View or Change Collection Set Schedules (SQL Server Management Studio)"
title: "View or change collection set schedules"
ms.date: 06/03/2020
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.dc.collectionsetprop.uploads.f1"
  - "sql13.swb.dc.collectionsetprop.description.f1"
  - "sql13.swb.dc.collectionsetprop.general.f1"
helpviewer_keywords: 
  - "collection sets [SQL Server], changing schedules"
  - "schedules [SQL Server], changing collection set"
  - "collection sets [SQL Server], viewing schedules"
  - "schedules [SQL Server], viewing collection set"
ms.assetid: 26336c98-78c5-414f-8d6a-574fc3af60c4
author: MashaMSFT
ms.author: mathoma
ms.custom: "seo-lt-2019"
---
# View or Change Collection Set Schedules (SQL Server Management Studio)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  You can view or change collection set schedules by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 The collection mode, cached or non-cached, determines how you can make changes to a schedule. Cached mode uses separate schedules for collection and upload. Non-cached mode uses the same schedule for collection and upload. The type of collection mode for each of the System Datacollection sets is as follows:  
  
-   **Disk Usage** uses non-cached collection mode.  
  
-   **Query Statistics** uses cached collection mode.  
  
-   **Server Activity** uses cached collection mode.  
  
### To view collection set schedules  
  
1.  In Object Explorer, expand the **Management** node, expand **Data Collection**, and then expand **System Data Collection Sets**.  
  
2.  Right-click a collection set name, and then click **Properties** to open the [Data Collection Set Properties](#CollectionSet) dialog box.  
  
### To change the schedules for a cached mode collection set  
  
1.  In Object Explorer, expand the **Management** node, expand **Data Collection**, and then expand **System Data Collection Sets**.  
  
2.  Right-click a collection set that uses cached mode, such as **Query Statistics**, and then click **Properties** to open the [Data Collection Set Properties](#CollectionSet) dialog box.  
  
3.  You can change the collection frequency on the **General** page. To do this, follow these steps:  
  
    1.  In the details pane, double-click the number that is displayed for the **Collection Frequency (sec)** column in the **Collection items** table.  
  
    2.  To increase or decrease the collection frequency, type a lower or higher number, and then press ENTER to store the new value.  
  
4.  To change the existing upload schedule for the collection set, follow these steps:  
  
    1.  Click the **Uploads** page.  
  
    2.  In the details pane, click **Pick**.  
  
         The **Pick Schedule for Job** dialog box opens. The available schedules appear as a table.  
  
    3.  Click the row with the schedule that you want. For example, to change the schedule to every 5 minutes, click the row where the schedule name is **CollectorSchedule_Every_5min.**  
  
        > [!NOTE]  
        >  You can view and edit the properties for the selected schedule by clicking **Properties** to open the **Job Schedule Properties** dialog box for the schedule. You can use this dialog box to change schedule information, such as the frequency.  
        >   
        >  As an alternative to modifying an existing schedule, you can create a new upload schedule by clicking **New** on the **Uploads** page. This action opens the **New Job Schedule** dialog box, which you can use to create a custom schedule.  
  
    4.  When you are finished configuring the schedule, click **OK**.  
  
         The changes that you made appear on the **Uploads** page.  
  
5.  Click **OK** to save the changes to the collection frequency and the upload schedule, and to close the **Data Collection Set Properties** dialog box.  
  
### To change the schedule for a non-cached mode collection set  
  
1.  In Object Explorer, expand the **Management** node, expand **Data Collection**, and then expand **System Data Collection Sets**.  
  
2.  Right-click a collection set that uses non-cached mode, such as **Disk Usage**, and then click **Properties** to open the [Data Collection Set Properties](#CollectionSet) dialog box.  
  
     The **Data Collection Set Properties** dialog box displays a paged view of the collection set properties.  
  
3.  To change the schedule for the collection set, click **Pick**.  
  
     The **Pick Schedule for Job** dialog box opens. The available schedules are displayed as a table.  
  
4.  Click the row with the schedule that you want. For example, to change the schedule to every 5 minutes, click the row where the schedule name is **CollectorSchedule_Every_5min**.  
  
    > [!NOTE]  
    >  You can view and edit the properties for the selected schedule by clicking **Properties** to open the **Job Schedule Properties** dialog box for the schedule. You can use this dialog box to change schedule information, such as the frequency.  
    >   
    >  As an alternative to modifying an existing schedule, you can create a new collect and upload schedule by clicking **New** on the **General** page. This action opens the **New Job Schedule** dialog box.  
  
5.  When you are finished configuring the schedule, click **OK**.  
  
6.  Click **OK** to save the changes and to close the **Data Collection Set Properties** dialog box.  
  
####  <a name="CollectionSet"></a> Data Collection Set Properties Dialog Box  
 **General Page**  
  
 Use this page to configure how data is collected and uploaded, configure schedules, and configure data retention periods in the management data warehouse. This page also provides information about collection sets, such as collector types and collection frequencies, as well as the input parameters that are used for a collection set.  
  
 **Name**  
 Displays the name of the collection set that this page refers to.  
  
 **Data collection and upload**  
 Specifies how data is collected and uploaded to the management data warehouse. Pick one of the following options.  
  
| Option | Description |
| :----- | :---------- |
|**Non-cached. Collection and data upload on the same schedule.**|When selected, specify one of the following:<br /><br /> **Schedule**. Data is collected and uploaded according to a schedule. Click **Pick** to select from a predefined list of schedules, or click **New** to create a new schedule.<br /><br /> **On-demand**. Data is collected and uploaded on demand.|  
|**Cached. Collect and cache data at a set of collection frequencies. Upload cached data on a separate schedule.**|Collect and cache data for a specified collection frequency. Upload the collected data on a separate schedule.|  
  
 **Collection items**  
 Displays the collection items in the collection set. The following information is provided for each collection item:  
  
-   **Name**  
  
-   **Collector Type**  
  
-   **Collection Frequency** (secs). This field is editable if **Data collection and upload** is configured as cached. Double-click this cell to set the collection frequency.  
  
 **Input parameters**  
 Displays the input parameters used for the collection set.  
  
 **Run as**  
 Specifies the account used to run the collection set. By default this is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Service Account. If proxy accounts are defined, this list includes the names of the available proxy accounts.  
  
 **Set how long collected data will be retained in the management data warehouse.**  
 Specifies how long collected data is retained. Pick one of the following options.  
  
| Option | Description |
| :----- | :---------- |
|**Retain data for**|This option is selected by default, and the default retention period is 14 days.|  
|**Retain data indefinitely**|There is no time limit on the length of time that data is retained.|  

 **Uploads Page**  
  
 Use this page to configure the upload schedule for data that is collected by this collection set.  
  
> [!NOTE]  
>  This tab is only enabled if the **Cached** option is configured for **Data collection and upload**.  
  
 **Server**  
 Displays the name of the server that hosts the management data warehouse. For more information, see [Configure the Management Data Warehouse &#40;SQL Server Management Studio&#41;](../../relational-databases/data-collection/configure-the-management-data-warehouse-sql-server-management-studio.md).  
  
 **Management data warehouse**  
 Displays the name of the management data warehouse. For more information, see [Configure the Management Data Warehouse &#40;SQL Server Management Studio&#41;](../../relational-databases/data-collection/configure-the-management-data-warehouse-sql-server-management-studio.md).  
  
 **Last upload**  
 Displays date and time information for the last upload done for the collection set.  
  
 **Upload schedule**  
 Specifies the upload schedule for the collection set. If enabled, Click **Pick** to select from a predefined list of schedules, or click **New** to create a new schedule.  
  
 **Description Page**  
  
 Use this page to view a description of the collection set that this properties page refers to.  
  
## See Also  
 [Manage Data Collection](../../relational-databases/data-collection/manage-data-collection.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)  
  
  
