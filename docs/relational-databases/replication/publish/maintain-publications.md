---
title: Maintain Publications
description: Maintain publications after you create them in SQL Server replication. Discover which property changes require a new snapshot and subscription reinitialization.
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
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Maintain publications
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  After you create a publication, you might need to add or remove articles, or change publication and article properties. You can make most changes after you create a publication. However, some changes require you to generate a new snapshot for a publication and reinitialize subscriptions to the publication.
