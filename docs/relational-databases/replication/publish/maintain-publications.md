---
title: "Maintain Publications"
description: "Maintain Publications"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "maintaining publications [SQL Server replication]"
  - "publications [SQL Server replication], maintaining"
  - "administering replication, publications"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Maintain Publications
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  After you have created a publication, it might be necessary to add or drop articles, or change publication and article properties. Most changes are allowed after a publication is created, but in some cases, it is necessary to generate a new snapshot for a publication and/or reinitialize subscriptions to the publication.
