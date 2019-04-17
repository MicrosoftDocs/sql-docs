---
title: "Project Settings (Migration) (DB2ToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 48aaa8e6-a9cb-487d-9ba5-fc3f1c4786ae
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Project Settings (Migration) (DB2ToSQL)
The Migration page of the **Project Settings** dialog box contains settings that customize how SSMA migrates data from DB2 to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
The Migration pane is available in both the **Project Settings** and **Default Project Settings** dialog boxes.  
  
-   To specify settings for all SSMA projects, on the **Tools** menu, select **Default Project Settings**, select migration project type for which settings are required to be viewed  or changed from **Migration Target Version** drop down click **General** at the bottom of the left pane, and then click **Migration**.  
  
-   To specify settings for the current project, on the **Tools** menu, select **Project Settings**, click **General** at the bottom of the left pane, and then click **Migration**.  
  
## Migration Engine  
  
|Term|Definition|  
|--------|--------------|  
|**Migration Engine**|Specifies database engine used during Data Migration. Client side data migration refers to SSMA client retrieving the data from the source and bulk inserting that data into SQL Server. Server side data migration refers to SSMA data migration engine (bulk copy program) running on the SQL Server box as a SQL Agent job retrieving data from the source and inserting directly into SQL Server  thus avoiding an extra client-hop (better performance).<br /><br />**Default Mode**:  Client Side Data Migration Engine<br /><br />**Optimistic Mode**:  Client Side Data Migration Engine<br /><br />**Full Mode**:  Client Side Data Migration Engine|  
  
> [!IMPORTANT]  
> When the **Migration Engine** option is set to **Server Side Data Migration Engine**, a new Project setting option **Use 32-Bit Server Side Data Migration Engine** is displayed. It specifies whether 32 bit or 64 bit Bulk Copy Program (BCP) utility is used to migrate data.  
  
## Miscellaneous Options  
  
|Term|Definition|  
|--------|--------------|  
|**Batch Size**|Specifies the batch size used during data migration.<br /><br />**Default Mode**:  10000<br /><br />**Optimistic Mode**:  10000<br /><br />**Full Mode**:  10000|  
|**Check constraints**|Specifies whether SSMA should check constraints when it inserts data into SQL Server tables.<br /><br />**Default Mode**:  False<br /><br />**Optimistic Mode**:  False<br /><br />**Full Mode**:  False|  
|**Data Migration Timeout**|Specifies the timeout used during data migration<br /><br />**Default Mode**:  15<br /><br />**Optimistic Mode**:  15<br /><br />**Full Mode**:  15|  
|**Extended Data Migration Options**|Shows extra data migration options for each table in separate detail tab.<br /><br />**Default Mode**:  Hide<br /><br />**Optimistic Mode**:  Hide<br /><br />**Full Mode**:  Hide|  
|**Fire triggers**|Specifies whether SSMA should fire insertion triggers when it adds data to SQL Server tables.<br /><br />**Default Mode**:  False<br /><br />**Optimistic Mode**:  False<br /><br />**Full Mode**:  False|  
|**Keep identity**|Specifies whether SSMA preserves null values in the source data when it adds data to SQL Server, regardless of the default values that are specified in SQL Server.<br /><br />**Default Mode**:  True<br /><br />**Optimistic Mode**:  True<br /><br />**Full Mode**:  False|  
|**Keep nulls**|Specifies whether SSMA preserves null values in the source data when it adds data to SQL Server, regardless of the default values that are specified in SQL Server.<br /><br />**Default Mode**:  True<br /><br />**Optimistic Mode**:  True<br /><br />**Full Mode**:  True|  
|**Mark string Trim operation with error**|If the target column size is less than the source string length, the value will be trimmed and marked as an error.<br /><br />**Default Mode**:  Yes<br /><br />**Optimistic Mode**:  Yes<br /><br />**Full Mode**:  Yes|  
|**On Error**|Stops Data migration when an error occurs. It has three options:<br /><br />**Stop migration:** Stops data migration operation<br /><br />**Proceed to next table:** Stops data migration to the current table and proceeds to the next one<br /><br />**Proceed to next batch:** Stops data migration to the current batch and proceeds to the next one<br /><br />**Default Mode**: Proceed to the next batch<br /><br />**Optimistic Mode**: Proceed to the next batch<br /><br />**Full Mode**: Proceed to the next batch|  
|**Replace unsupported dates**|Specifies whether SSMA should correct dates that are earlier than the earliest [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **datetime** date (01 January 1753).<br /><br />To keep the current date values, select **Do nothing**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will not accept dates before 01 January 1753 in a datetime column. If you use older dates, you must convert the datetime values to character values.<br /><br />To convert dates before 01 January 1753 to NULL, select **Replace with NULL**.<br /><br />To replace dates before 01 January 1753 with a supported date, select **Replace with nearest supported date**.<br /><br />**Default Mode**: Do nothing<br /><br />**Optimistic Mode**: Do nothing<br /><br />**Full Mode**: Replace with nearest supported date|  
|**Table lock**|Specifies whether SSMA locks tables when it adds data to tables during data migration. Obtains a bulk update lock for the duration of the bulk copy operation. If the value is False, a lock is set at the row level.<br /><br />**Default Mode**:  True<br /><br />**Optimistic Mode**:  True<br /><br />**Full Mode**:  True|  
  
## Parallel Data Migration  
  
|Term|Definition|  
|--------|--------------|  
|**Parallel Data Migration Mode**|Specifies the mode used to fork threads to enable parallel data migration. In Auto mode, SSMA chooses the number of threads (10 by default) forked to migrate data. In Custom mode, user can specify the number of threads forked to migrate data (minimum is 1 and maximum is 100). Currently, only client side data migration engine supports parallel data migration.<br /><br />**Default Mode**:  Auto<br /><br />**Optimistic Mode**:  Auto<br /><br />**Full Mode**:  Auto|  
  
> [!IMPORTANT]  
> When the **Parallel Data Migration Mode** option is set to **Custom**, a new Project setting option **Thread Count** is displayed. It specifies number of threads used for data migration.  
  
