---
title: Save Metadata
description: Learn about the Save Metadata dialog box that prompts you to save metadata before saving your SSMA project.
author: cwebb-msft
ms.author: chweb
ms.date: 04/16/2026
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
ai-usage: ai-assisted
---
# Save Metadata

The **Save Metadata** dialog box prompts you to load metadata into your SQL Server Migration Assistant (SSMA) project before saving it. This lets you have a complete project file that you can use offline and send to other people, such as technical support personnel.

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

Clears the check box for all databases with missing metadata. You can't clear the check box if metadata is loaded.

#### Save

Saves the project and adds metadata for selected databases that are missing metadata.

#### Cancel

Cancels the save operation. Missing metadata isn't loaded into the project.

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
