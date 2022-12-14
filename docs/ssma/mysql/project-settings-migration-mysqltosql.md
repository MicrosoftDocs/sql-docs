---
description: "Project Settings (Migration) (MySQLToSQL)"
title: "Project Settings (Migration) (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom:
  - intro-migration
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 2a3cba9e-cd54-4a8b-b858-8fc4cf2580d9
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.mysql.projectsettingmigration.f1"
---
# Project Settings (Migration) (MySQLToSQL)
The Migration page of the **Project Settings** dialog box contains settings that customize how SSMA migrates data from MySQL to SQL Server.  
  
The Migration pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.  
  
-   To specify settings for all SSMA projects, on the **Tools** menu, select **Default Project Settings**, select the project type in **Migration Target Version** drop down of which you want to access the settings, click **General** at the bottom of the left pane, and then click **Migration**.  
  
-   To specify settings for the current project, on the **Tools** menu, select **Project Settings**, click **General** at the bottom of the left pane, and then click **Migration**.  
  
## Options  
  
### Bulk Copy  
  
|Term|Definition|  
|--------|--------------|  
|**Batch Size**|Specifies the batch size used during data migration.<br /><br />**Default Mode**:  1000<br /><br />**Optimistic Mode**:  1000<br /><br />**Full Mode**:  1000|  
|**Check constraints**|Specifies whether SSMA should check constraints when it inserts data into SQL Server tables.<br /><br />**Default Mode**:  False<br /><br />**Optimistic Mode**:  False<br /><br />**Full Mode**:  False|  
|**Fire triggers**|Specifies whether SSMA should fire insertion triggers when it adds data to SQL Server tables.<br /><br />**Default Mode**:  False<br /><br />**Optimistic Mode**:  False<br /><br />**Full Mode**:  False|  
|**Keep identity**|Specifies whether SSMA preserves MySQL identity values when it adds data to SQL Server. A value of False causes identity values to be assigned by the destination.<br /><br />**Default Mode**:  True<br /><br />**Optimistic Mode**:  True<br /><br />**Full Mode**:  True|  
|**Keep nulls**|Specifies whether SSMA preserves null values in the source data when it adds data to SQL Server, regardless of the default values that are specified in SQL Server.<br /><br />**Default Mode**:  True<br /><br />**Optimistic Mode**:  True<br /><br />**Full Mode**:  True|  
|**Table lock**|Specifies whether SSMA locks tables when it adds data to tables during data migration. Obtains a bulk update lock for the duration of the bulk copy operation. If the value is False, a lock is set at the row level.<br /><br />**Default Mode**:  False<br /><br />**Optimistic Mode**:  False<br /><br />**Full Mode**:  False|  
  
### Data Modification  
  
|Term|Definition|  
|--------|--------------|  
|**Invalid Dates Migration**|Specifies how to migrate invalid dates with like '2007-04-23' or '2000-06-31 10:00:00' in DATE and DATETIME formats.<br /><br />**Default Mode**:  Set NULL<br /><br />**Optimistic Mode**:  Set NULL<br /><br />**Full Mode**:  Set NULL|  
|**Negative TIME values Migration**|Specifies how to migrate negative values like '-30:11:00' in TIME columns.<br /><br />**Default Mode**:  Set NULL<br /><br />**Optimistic Mode**:  Set NULL<br /><br />**Full Mode**:  Set NULL|  
|**TIME values over 24 hours Migration**|Specifies how to migrate TIME values of more than '23:59:59' in TIME columns.<br /><br />**Default Mode**:  Set NULL<br /><br />**Optimistic Mode**:  Set NULL<br /><br />**Full Mode**:  Set NULL|  
|**Truncate binary values to fit into column**|If Yes, SSMA truncates binary values from MySQL that do not fit into SQL table columns and generates appropriate error message. If No, the row causes an error<br /><br />**Default Mode**:  No<br /><br />**Optimistic Mode**:  No<br /><br />**Full Mode**:  No|  
|**Truncate character values to fit into column**|SSMA truncates character values from MySQL that do not fit into SQL table columns and generates appropriate error message.<br /><br />**Default Mode**:  No<br /><br />**Optimistic Mode**:  No<br /><br />**Full Mode**:  No|  
|**Zero Dates Migration**|Specifies how to migrate zero dates like '0000-00-00' or '0000-00-00 00:00:00' in DATE and DATETIME columns.<br /><br />**Default Mode**:  Set NULL<br /><br />**Optimistic Mode**:  Set NULL<br /><br />**Full Mode**:  Set NULL|  
|**Zero in Dates Migration**|Specifies how to migrate dates with zero parts like '2009-01-00' or '2000-00-00 11:00:00' in DATE and DATETIME columns.<br /><br />**Default Mode**:  Set NULL<br /><br />**Optimistic Mode**:  Set NULL<br /><br />**Full Mode**:  Set NULL|  
  
### Migration Engine  
  
|Term|Definition|  
|--------|--------------|  
|**Migration Engine**|Specifies database engine used during Data Migration. Client side data migration refers to SSMA client retrieving the data from the source and bulk inserting that data into SQL Server. Server side data migration refers to SSMA data migration engine (bulk copy program) running on the SQL Server box as a SQL Agent job retrieving data from the source and inserting directly into SQL Server  thus avoiding an extra client-hop (better performance).<br /><br />**Default Mode**:  Client Side Data Migration Engine<br /><br />**Optimistic Mode**:  Client Side Data Migration Engine<br /><br />**Full Mode**:  Client Side Data Migration Engine|  
  
> [!IMPORTANT]  
> When the **Migration Engine** option is set to **Server Side Data Migration Engine**, a new Project setting option **Use 32-Bit Server Side Data Migration Engine** is displayed. It specifies whether 32 bit or 64 bit Bulk Copy Program (BCP) utility is used to migrate data.  
  
### Misc  
  
|Term|Definition|  
|--------|--------------|  
|**Extended Data Migration Options**|Shows extra data migration options for each table in separate detail tab.<br /><br />**Default Mode**:  Hide<br /><br />**Optimistic Mode**:  Hide<br /><br />**Full Mode**:  Hide|  
|**On Error**|Stops Data migration when an error occurs. It has three options:<br /><br />**Stop migration:** Stops data migration operation<br /><br />**Proceed to next table:** Stops data migration to the current table and proceeds to the next one<br /><br />**Proceed to next batch:** Stops data migration to the current batch and proceeds to the next one<br /><br />**Default Mode**: Proceed to the next batch<br /><br />**Optimistic Mode**: Proceed to the next batch<br /><br />**Full Mode**: Proceed to the next batch|  
  
### Parallel Data Migration  
  
|Term|Definition|  
|--------|--------------|  
|**Parallel Data Migration Mode**|Specifies the mode used to fork threads to enable parallel data migration. In Auto mode, SSMA chooses the number of threads (10 by default) forked to migrate data. In Custom mode, user can specify the number of threads forked to migrate data (minimum is 1 and maximum is 100). Currently, only client side data migration engine supports parallel data migration.<br /><br />**Default Mode**:  Auto<br /><br />**Optimistic Mode**:  Auto<br /><br />**Full Mode**:  Auto|  
  
> [!IMPORTANT]  
> When the **Parallel Data Migration Mode** option is set to **Custom**, a new Project setting option **Thread Count** is displayed. It specifies number of threads used for data migration.  
  
### Spatial Data  
  
|Term|Definition|  
|--------|--------------|  
|**Handling Errors**|Specifies how to handle errors in migration of values of spatial data types. If 'Replace with NULL' is specified, all spatial values causing errors will be replaced with NULL. No replacement is done otherwise.<br /><br />**Default Mode**:  Generate an Error<br /><br />**Optimistic Mode**:  Generate an Error<br /><br />**Full Mode**:  Generate an Error|  
|**Value Validation**|Specifies how to handle invalid spatial values. If 'Try Make Valid' is specified, an attempt is being made to modify invalid values to make them valid.<br /><br />**Default Mode**: Make Valid<br /><br />**Optimistic Mode**: Do Not Change<br /><br />**Full Mode**: Make Valid|  
  
