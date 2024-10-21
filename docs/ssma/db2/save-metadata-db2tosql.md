---
title: "Save Metadata (Db2ToSQL)"
description: Save Metadata lets you load metadata into your SSMA for Db2 project before saving it.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Save Metadata (Db2ToSQL)

The **Save Metadata** dialog box prompts you to load metadata into your SQL Server Migration Assistant (SSMA) project before saving it. With this feature, you have a complete project file that you can use offline and send to other people, such as technical support personnel.

To access the **Save Metadata** dialog box, save the project. If any metadata is missing, SSMA displays the **Save Metadata** dialog box.

## Options

#### Name

The name of each database in the project.

#### Status

Indicates if metadata is loaded into the SSMA project, or if metadata is missing.

SSMA loads metadata into the project as necessary. Metadata is loaded automatically when you browse metadata and convert schemas.

#### Select All

Selects all listed databases.

#### Clear

Clears the check box for all databases with missing metadata. You can't clear the check box if metadata was loaded.

#### Save

Saves the project, loading metadata for selected databases that have missing metadata.

#### Cancel

Cancels the save operation. Missing metadata isn't loaded into the project.
