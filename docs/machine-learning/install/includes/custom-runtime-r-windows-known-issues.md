---
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 04/07/2021
ms.topic: include
author: anmunde
ms.author: anmunde
ms.reviewer: rothja
---
## Known issues

If you're using the R runtime provided as part of [SQL Server Machine Learning Services](../../sql-server-machine-learning-services.md) by setting `R_HOME` to `C:\Program Files\Microsoft SQL Server\MSSQL15.<INSTANCE_NAME>\R_SERVICES` when you register the language extension, you might run into the following error upon executing any external custom R script with [sp_execute_external script](../../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

*Error: cons memory exhausted (limit reached?)*

To resolve this issue:
 1. Set the environment variable `R_NSIZE` indicating the number of fixed sized objects (`cons cells`) to a reasonable value, for example, `200000`.
 1. Restart the **Launchpad** service and retry the execution of the script.