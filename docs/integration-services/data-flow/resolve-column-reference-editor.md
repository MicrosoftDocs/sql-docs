---
title: "Resolve Column Reference Editor"
description: "Resolve Column Reference Editor"
author: chugugrace
ms.author: chugu
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: ui-reference
f1_keywords:
  - "sql13.dts.designer.resolvereferences.preview.F1"
  - "sql13.dts.designer.resolvereferences.mapper.F1"
---
# Resolve Column Reference Editor

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  When an input path is disconnected or if there are any unmapped columns in the path, an error icon is displayed beside the corresponding data path. To simplify the resolution of column reference errors, the Resolve References editor allows you to link unmapped output columns with unmapped input columns for all paths in the execution tree. The Resolve References editor will also highlight the paths to indicate which paths are being resolved.  
  
> [!NOTE]  
>  It is possible to edit a component even when its input path is disconnected  
  
 After all column references have been resolved, if there are no other data path errors, no error icons will be displayed beside the data paths.  
  
## Options  
 **Unmapped Output Columns (Source)**    
 Columns from the upstream path that are not currently mapped  
  
**Mapped Columns (Source)**    
 Columns from the upstream path that are mapped to columns from the downstream path  
  
**Mapped Columns (Destination)**    
 Columns from the upstream path that are mapped to columns from the downstream path  
  
**Unmapped Input Columns (Destination)**    
 Columns from the downstream path that are not currently mapped  
  
**Delete Unmapped Input Columns**  
 Check Delete Unmapped Input Columns to ignore unmapped columns at the destination of the data path. The Preview Changes button displays a list of the changes that will occur when you press the OK button.  
  
  
