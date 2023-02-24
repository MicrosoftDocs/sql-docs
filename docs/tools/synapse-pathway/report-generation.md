---
title: Azure Synapse Pathway Report Generation
description: Azure Synapse Pathway provides comprehensive reporting on scripts translated.
author: WilliamDAssafMSFT 
ms.author: wiassaf 
ms.topic: tutorial
ms.service: sql
ms.subservice: tools-other 
ms.date: 09/09/2021
monikerRange: "=azure-sqldw-latest"
ms.custom: template-tutorial 
---

# Report Generation for Azure Synapse Pathway
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway provides a comprehensive report of the number of scripts that were successfully translated. The report also shows the number of errors and warnings on statements that didn't get translated.

## Generate Report

When you select **Translate**, you're presented with a report like below:

![Azure Synapse Pathway report.](./media/report-generaration/report-overview.png)

## Report Summary

The Conversion Success will show the percentage of scripts translated successfully.

![Azure Synapse pathway.](./media/report-generaration/conversion-success.png)

The Statement Distribution section details the number of DDL and DML statements that have been translated, including the number of schemas, tables, and databases created.

![Azure Synapse report1.](./media/report-generaration/statement-distribution.png)

The migration savings are calculated based on the number  and complexity, simple, high, or medium, of objects translated. Synapse Pathway assumes 30 mins of manual effort for each object translation.

![Azure Synapse report2.](./media/report-generaration/migration-savings.png)

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)