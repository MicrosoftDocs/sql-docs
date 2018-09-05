---
title: Create a data pool from a Transact-SQL query | Microsoft Docs
description:
author: rothja
ms.author: jroth
manager: craigg
ms.date: 08/16/2018
ms.topic: conceptual
ms.prod: sql
---

# Create a data pool from a Transact-SQL query

Create a data pool from a T-SQL query – this sample shows how you can extract some data from an external data source and shard it across a data pool for caching the data.  

> [!NOTE]
> This sample is relatively simple for now because the external data source connectors are not ready yet.

This sample just gets some text out of sys.messages on the master instance and loads it into the data pool.  Check out the file in GitHub at **CTP2.0/sample-tsql/run_data_pool_table_scenario_as_demo.sql**.

## Next steps

TBD