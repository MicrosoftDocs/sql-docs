---
title: azdata sql
titleSuffix: SQL Server big data clusters
description: Reference article for azdata sql commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/21/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# `azdata sql`

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the `sql` shell in the `azdata` tool. For more information about other `azdata` commands, see [azdata reference](reference-azdata.md).

## Commands

|     |     |
| --- | --- |
|`azdata sql query` | The query command allows execution of a T-SQL query.
|`azdata sql shell` | The SQL DB CLI allows the user to interact with SQL Server via T-SQL.

## More information

Requires login to controller. The following example, logs a user into a cluster, and connects to the controller, then initiates `azdata sql` shell.

```bash
azdata login --cluster-name ClusterName --controller-user johndoe@contoso.com  --controller-endpoint https://<ip>:30080 --accept-eula yes

azdata sql shell
```

The SQL Server `sa` or controller password is needed. It can be set for the session by setting the environment variable `MSSQL_SA_PASSWORD`.

If SQL Server sa password is not set then `CONTROLLER_PASSWORD` is used.

By default, `mssql-cli` collects usage data in order to improve your experience.

The data is anonymous and does not include command-line argument values.

The data is collected by Microsoft.

Disable telemetry collection by setting environment variable `MSSQL_CLI_TELEMETRY_OPTOUT` to '`True` or `1`.

Refer to [Microsoft Privacy statement](https://privacy.microsoft.com/privacystatement).

## Next steps

For more information about other `azdata` commands, see [azdata reference](reference-azdata.md). For more information about how to install the `azdata` tool, see [Install azdata to manage [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](deploy-install-azdata.md).
