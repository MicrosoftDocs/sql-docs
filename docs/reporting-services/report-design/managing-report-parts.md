---
title: "Managing Report Parts | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 41947b4c-8ecf-4e4f-b30e-66e1d6692b74
author: markingmyname
ms.author: maghan
---
# Managing Report Parts
  Report parts can be reused in paginated reports, by multiple users and in multiple reports. Users can search for report parts on the server and add them to a report.  Users can also be informed of updates to the report part on the server, and republish new versions of a report part. Those report authoring actions can be affected by and controlled by reporting services security permissions.  This topic reviews report part properties and behavior after they are on the server.  
  
## Managing Report Parts  
 To manage report parts, you can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal for a report server in native mode, or application pages for a report server in SharePoint integrated mode.  
  
### Server-side interaction and search  
 Report parts can be published to a report server in either native mode or SharePoint integrated mode. Users can use the report part gallery feature in a report authoring application such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Report Builder to find and add report parts to their reports. When a user searches for a report part, the search looks at the report server catalog regardless of which mode the server was installed for.  
  
 When report parts are published from a report authoring application such as Report Builder to a report server in SharePoint integrated mode, the report server catalog is also updated, and searches from the gallery to accurately reflect the new or update report part.  
  
#### Directly uploading report parts to a SharePoint folder  
 If a report part is uploaded directly to a SharePoint document folder instead of being published from a report authoring application, the report server catalog is not updated. Searches from the report part gallery will not find the uploaded report part. To help keep your SharePoint folders and report server catalog synchronized, you can activate the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] file sync feature on the SharePoint server. For more information, see [Activate the Report Server File Sync Feature in SharePoint Central Administration](../../reporting-services/report-server-sharepoint/activate-the-report-server-file-sync-feature-in-sharepoint-ca.md).  
  
 The files can also be synchronized by calling some of the reporting services management APIs such as GetProperties and SetProperties.  
  
### Organizing and moving report parts  
 You should consider and plan ahead for how your team will use and organize report parts, shared datasets, and shared data sources. Although you can move them at a later time, there can be problems.  
  
#### Native mode report server  
 If you move a report part on a native mode report server to any other folder on the same server, it does not affect the ability of report authoring applications to search for or download updates of the report part. This is because the server relies on the unique ComponentID. However, if the report part is moved to a folder that a user does not have permissions to, their searches will not find the report part, and their reports will not be notified when there are updates to the report part.  
  
#### Report server in SharePoint integrated mode  
 Moving report parts to a different document library or folder has the same effect as uploading them directly to a SharePoint server: the report server catalog will not be synchronized. To avoid this, activate the Report Server File Sync Feature on the SharePoint server.  
  
 The exception is subfolders. The search will always search subfolders, so if you manually move a report part to a subfolder, a search from the report gallery will find the report part. it will still be found in a search from the report part gallery.  
  
### Report server catalog properties  
 The following table describes how existing report server catalog fields relate to a report part, and to new fields that are added to the catalog for report parts. These are exposed in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal and SharePoint libraries, and in report authoring applications such as Report Builder.  
  
 (*) indicates it is new for this release.  
  
|Property|Description|Report Part<br /><br /> Gallery Search Criteria|  
|--------------|-----------------|---------------------------------------------|  
|Name|This is one of the criteria a user can search for in the Report Part Gallery.|Yes|  
|Description|You might want to organize report part names in a way that will make it easier for users to find in the gallery. For example, you can search for the description starting with "Sales>>" to find all report parts that involve sales related data and presentation.|Yes|  
|CreatedBy|The ID of the user that added the report part to the report server database. The exact format depends on the authentication method. For example, some authentication methods result in showing the full domain\user name in the CreatedBy and ModifiedBy fields.|Yes|  
|CreationDate|The date the report part was originally created.<br /><br /> This is one of the criteria a user can search for in the Report Part Gallery.|Yes|  
|ModifiedBy|ModifiedBy is the ID of the user who last modified the report part.|Yes|  
|ModifiedDate|The date the report part was last modified on the server.<br /><br /> This field is used as part of the logic to determine when there are server-side updates to a report part. For more information, see the description of the ComponentID later in this table.|Yes|  
|SubType (*)|SubType is a string that indicates the kind of report part to search for, such as "Tablix" or "Chart".|Yes|  
|ComponentID (*)|ComponentID is a unique identifier for the report part. This is a new field added to the catalog, and is visible on both the server-side and report authoring applications such as Report Builder.<br /><br /> This field is used by client applications when checking the server for updates of a report part. The client application searches the server for ComponentIDs that are in the current client-side report. When there is a ComponentID match, the ModifiedDate is then compared to the client side SyncDate of the report item.|N0|  
  
## Controlling access to report parts  
 The following tables describe the default role assignments and how they let you perform various operations. The role assignment names are different depending on the type of report server that is being used.  
  
### Server in Native mode  
  
|Actions|Roles|  
|-------------|-----------|  
|Add, delete, edit item properties, manage security, and download report parts|Content Manager<br /><br /> My Reports|  
|Add, delete, and download report parts|Publisher|  
|Search for and re-use|Browser<br /><br /> Report Builder|  
  
### Server in SharePoint integrated mode  
  
|Actions|Role|  
|-------------|----------|  
|Add, delete, edit item properties, manage security, and download report parts|Full Control|  
|Add, delete, edit item properties, and download report parts|Design<br /><br /> Contribute|  
|Search for and re-use|Read<br /><br /> View Only|  
  
### Security considerations  
  
-   When report part definitions are re-used in a report, they are copied into the report definition in their entirety, along with the identifying ComponentID. If a report part is updated on the server, users can choose to download the updated report parts to their report. The updates are also complete copies of the report part, replacing the existing version of the report part that was in their report.  
  
    > [!IMPORTANT]  
    >  In each of these steps, ensure that the report parts are being reused in reports from trusted locations and users.  
  
-   Report parts use the same permission policies as the existing "Resource" item type. Within a folder, there is no differentiation between traditional resource items and report parts from a security inheritance perspective. The report part will inherit the same permission policy as the images in the same folder. When this distinction is needed, item level security can be configured for the desired report parts. Or, you can put report parts should be in separate folders that have the correct permissions configured.  
  
## See Also  
 [Report Parts and Datasets in Report Builder](../../reporting-services/report-data/report-parts-and-datasets-in-report-builder.md)   
 [Report Server Content Management &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Troubleshoot Report Parts (Report Builder and SSRS)](https://msdn.microsoft.com/d9fe1932-46e7-421b-a8a9-4c54d9576e94)   
 [Report Parts in Report Designer &#40;SSRS&#41;](../../reporting-services/report-design/report-parts-in-report-designer-ssrs.md)  
  
  
