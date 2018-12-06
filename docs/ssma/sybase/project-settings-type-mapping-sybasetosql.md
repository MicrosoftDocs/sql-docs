---
title: "Project Settings (Type Mapping) (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 2698fb3a-f9e6-4e04-94e0-dad289d7ed0a
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Project Settings (Type Mapping) (SybaseToSQL)
The Type Mapping page of the **Project Settings** dialog box contains settings that customize how SSMA converts Sybase Adaptive Server Enterprise (ASE) data types into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.  
  
The Type Mapping page is available in the **Project Settings** and **Default Project Settings** dialog boxes.  
  
-   To specify the type mapping settings for all future SSMA projects, on the **Tools** menu, select **Default Project Settings**, select migration project type for which settings are required to be viewed or changed from **Migration Target Version** drop down and then select **Type Mapping** at the bottom of the left pane.  
  
-   To specify settings for the current project, on the **Tools** menu, select **Project Settings**, and then select **Type Mapping** at the bottom of the left pane.  
  
## Options  
**Source Type**  
The mapped ASE data type.  
  
**Target Type**  
The target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type for the specified ASE data type.  
  
See the table in the following section for the default SSMA for Sybase type mapping.  
  
**Add**  
Click to add a data type to the mapping list.  
  
**Edit**  
Click to edit the selected data type in the mapping list.  
  
**Remove**  
Click to remove the selected data type mapping from the mapping list.  
  
**Reset to Default**  
Click to reset the type mapping list to the SSMA defaults.  
  
## Default Type Mapping  
The following table contains the default type mapping between ASE and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.  
  
|ASE Data Type|SQL Server Data Type|  
|-----------------|------------------------|  
|**bigint**|**bigint**|  
|**binary**|**binary**|  
|**binary[\*..8000]**|**binary[\*]**|  
|**binary[8001..\*]**|**varbinary(max)**|  
|**bit**|**bit**|  
|**char**|**char**|  
|**char varying**|**varchar**|  
|**char varying[\*..8000]**|**varchar[\*]**|  
|**char varying[8001..\*]**|**varchar(max)**|  
|**char[\*..8000]**|**char[\*]**|  
|**char[8001..\*;]**|**varchar(max)**|  
|**character**|**char**|  
|**character varying**|**varchar**|  
|**character varying[\*..8000]**|**varchar[\*]**|  
|**character varying[8001..\*]**|**varchar(max)**|  
|**character[\*..8000]**|**char[\*]**|  
|**character[8001..\*]**|**varchar(max)**|  
|**date**|**date**|  
|**datetime**|**datetime2[3]**|  
|**dec**|**decimal**|  
|**dec[\*..\*]**|**decimal[\*]**|  
|**dec[\*..\*][\*..\*]**|**decimal[\*][\*]**|  
|**decimal**|**decimal**|  
|**decimal[\*..\*]**|**decimal[\*]**|  
|**decimal[\*..\*][\*..\*]**|**decimal[\*][\*]**|  
|**double precision**|**float[53]**|  
|**float**|**float[53]**|  
|**float[\*..15]**|**float[24]**|  
|**float[16..\*]**|**float[53]**|  
|**image**|**image**|  
|**int**|**int**|  
|**integer**|**int**|  
|**longsysname**|**nvarchar[255]**|  
|**money**|**money**|  
|**national char**|**nchar**|  
|**national char[\*..4000]**|**nchar[\*]**|  
|**national char varying**|**nvarchar**|  
|**national char varying[\*..4000]**|**nvarchar[\*]**|  
|**national char varying[4001..\*]**|**nvarchar(max)**|  
|**national char[4001..\*]**|**nvarchar(max)**|  
|**national character**|**nchar**|  
|**national character[\*..4000]**|**nchar[\*]**|  
|**national character[4001..\*]**|**nvarchar(max)**|  
|**national character varying**|**nvarchar**|  
|**national character varying[\*..4000]**|**nvarchar[\*]**|  
|**national character varying[4001..\*]**|**nvarchar(max)**|  
|**national varchar**|**nvarchar**|  
|**national varchar[\*..4000]**|**nvarchar[\*]**|  
|**national varchar[4001..\*]**|**nvarchar(max)**|  
|**nchar**|**nchar**|  
|**nchar varying**|**nvarchar**|  
|**nchar varying[\*..4000]**|**nvarchar[\*]**|  
|**nchar varying[4001..\*]**|**nvarchar(max)**|  
|**nchar[\*..4000]**|**nchar[\*]**|  
|**nchar[4001..\*]**|**nvarchar(max)**|  
|**numeric**|**numeric**|  
|**numeric[\*..\*]**|**numeric[\*]**|  
|**numeric[\*..\*][\*..\*]**|**numeric[\*][\*]**|  
|**nvarchar**|**nvarchar**|  
|**nvarchar[\*..4000]**|**nvarchar[\*]**|  
|**nvarchar[4001..\*]**|**nvarchar(max)**|  
|**real**|**float[24]**|  
|**smalldatetime**|**smalldatetime**|  
|**smallint**|**smallint**|  
|**smallmoney**|**smallmoney**|  
|**sysname**|**nvarchar[128]**|  
|**sysname[\*..\*]**|**nvarchar[255]**|  
|**text**|**text**|  
|**time**|**time[3]**|  
|**timestamp**|**rowversion**|  
|**tinyint**|**tinyint**|  
|**unichar**|**nchar**|  
|**unichar varying**|**nvarchar**|  
|**unichar varying[\*..4000]**|**nvarchar[\*]**|  
|**unichar varying[4001..\*]**|**nvarchar(max)**|  
|**unichar[\*..4000]**|**nchar[\*]**|  
|**unichar[4001..\*]**|**nvarchar(max)**|  
|**unitext**|**nvarchar(max)**|  
|**univarchar**|**nvarchar**|  
|**univarchar[\*..4000]**|**nvarchar[\*]**|  
|**univarchar[4001..\*]**|**nvarchar(max)**|  
|**unsigned bigint**|**numeric[20][0]**|  
|**unsigned int**|**bigint**|  
|**unsigned smallint**|**int**|  
|**unsigned tinyint**|**tinyint**|  
|**varbinary**|**varbinary**|  
|**varbinary[\*..8000]**|**varbinary[\*]**|  
|**varbinary[8001..\*]**|**varbinary(max)**|  
|**varchar**|**varchar**|  
|**varchar[\*..8000]**|**varchar[\*]**|  
|**varchar[8001..\*]**|**varchar(max)**|  
  
