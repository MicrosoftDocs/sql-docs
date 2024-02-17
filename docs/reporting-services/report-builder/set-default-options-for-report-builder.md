---
title: "Set default options for Report Builder"
description: This article describes useful defaults that you can set in Report Builder. These defaults make authoring a report easier and faster.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "10427"
---
# Set default options for Report Builder

  In Report Builder, you can set many useful defaults to make report authoring easier and faster. For example, if you can set or change the default report server, Report Builder saves your reports to the same report server automatically, unless you specify otherwise.

- In Report Builder, select **File** and choose **Options**.

## UI element list

**Use this report server or SharePoint site by default**  
Your administrator might configure this setting. The value can be a well-formed URL starting with `http://` or `https://`. This setting determines which data source connections appear by default in the **Table/Matrix** and **Chart** wizards. In addition, your reports are processed on this server and you can reference resources from this server.

If you select a different report server, you might need to restart Report Builder in order for this change to take effect.

**Publish report parts to this folder by default**  
Report Builder saves report parts that you publish to this folder. If the folder doesn't exist yet and you have permissions to create folders on the report server, Report Builder creates this folder.

You don't need to restart Report Builder for this setting to take effect.

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]

**Show this number of recent sites and servers**  
Specify the number of recent sites and connections to show in the **Open Report** and **Save As Report** dialogs.

**Show this number of recent shared datasets and data source connections**  
Specify the number of recent shared datasets and data source connections to show in the **Dataset Properties** dialog and the **Choose a connection to a data source** page of the **Data Regions** Wizard.

**Show this number of recent documents**  
Specify the number of recent documents to show when you select the **Report Builder** button.

**Clear all recent item lists**  
Clear the current lists of recent sites and servers, shared datasets, shared data source connections, and documents.

## Related content

[Start Report Builder](../../reporting-services/report-builder/start-report-builder.md)
