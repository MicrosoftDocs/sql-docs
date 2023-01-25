---
description: "Maintain Publications"
title: "Maintain Publications | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "maintaining publications [SQL Server replication]"
  - "publications [SQL Server replication], maintaining"
  - "administering replication, publications"
ms.assetid: d5bf7340-2b0b-4593-965c-de04ae628344
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Maintain Publications
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  After you have created a publication, it might be necessary to add or drop articles, or change publication and article properties. Most changes are allowed after a publication is created, but in some cases, it is necessary to generate a new snapshot for a publication and/or reinitialize subscriptions to the publication.
