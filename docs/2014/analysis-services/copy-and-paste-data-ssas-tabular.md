---
title: "Copy and Paste Data (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
  - "analysis-services"
ms.topic: conceptual
f1_keywords:
  - "sql12.asvs.bidtoolset.pastepreviewdb.f1"
ms.assetid: 2f8d8b3d-810b-4c31-98f2-341015e13da8
author: minewiskan
ms.author: owend
manager: craigg
---
# Copy and Paste Data (SSAS Tabular)

You can copy table data from external applications and paste it into a new or existing table in the model designer. The data that you paste from the Clipboard must be in HTML format, for example, data that is copied from Excel or Word. The model designer will automatically detect and apply data types to the pasted data. You can also manually modify the data type or display formatting of a column.

Unlike tables with a data source connection, pasted tables do not have a Connection Name or Source Data property. Pasted data is persisted in the Model.bim file. When the project or Model.bim file is saved, the pasted data is also saved.

When a model is deployed, pasted data will also be deployed with it regardless if the model is processed with the deployment.

Sections in this topic:

- [Prerequisites](#bkmk_prerequisites)

- [Paste Data](#bkmk_paste_data)

- [Paste Preview Dialog Box](#bkmk_paste_preview)

## <a name="bkmk_prerequisites"></a> Prerequisites

There are some restrictions when pasting data:

- Pasted tables cannot have more than 10,000 rows.

- Pasted tables cannot be partitioned.

- Pasted tables are not supported in DirectQuery mode.

- The **Paste Append** and **Paste Replace** options are only available when working with a table that was initially created by pasting data from the Clipboard. You cannot use **Paste Append** or **Paste Replace** to add data into a table of imported data from another type of data source.

- When you use **Paste Append** or **Paste Replace**, the new data must contain exactly the same number of columns as the original data. Preferably, the columns of data that you paste or append should also be of the same or compatible data type as those in the destination table. In some cases you can use a different data type, but a **Type mismatch** error may be displayed.

## <a name="bkmk_paste_data"></a> Paste Data

#### To paste data into the designer

- In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click the **Edit** menu, and then click of the following:

    - Click **Paste** to paste the contents of the Clipboard into a new table.

    - Click **Paste Append** to paste the contents of the Clipboard as additional rows into the selected table. The new rows will be added to the end of the table.

    - Click **Paste Replace** to replace the selected table with the contents of the Clipboard. All existing column header names will remain in the table, and relationships are preserved.

## <a name="bkmk_paste_preview"></a> Paste Preview Dialog Box

The **Paste Preview** dialog box enables you to see a preview of data that is copied into the designer window and to ensure that the data is copied correctly. To access this dialog box, copy table-based data in the HTML format to the clipboard, and then in the designer, click on the **Edit** menu, and then click **Paste**, **Paste Append**, or **Paste Replace**. The **Paste Append** and **Paste Replace** options are only available when you add or replace data in a table that was created by copying and pasting from the Clipboard. You cannot use **Paste Append** or **Paste Replace** to add data into a table of imported data.

The options for this dialog box are different depending on whether you paste data into a completely new table, paste data into an existing table and then replace the existing data with the new data, or whether you append data to an existing table.

### Paste to New Table

**Table name**\
Specify the name of the table that will be created in the designer.

**Data to be pasted**\
Shows a sample of the Clipboard contents that will be added to the destination table.

### Paste Append

**Existing data in the table**\
Shows a sample of the existing data in the table so that you can verify the columns, data types, etc.

**Data to be pasted**\
Shows a sample of the Clipboard contents. The existing data will be appended by this data.

### Paste Replace

**Existing data in the table**\
Shows a sample of the existing data in the table so that you can verify the columns, data types, etc.

**Data to be pasted**\
Shows a sample of the Clipboard contents. The existing data in the destination table will be deleted and the new rows will be inserted into the table.

## See Also

- [Import Data &#40;SSAS Tabular&#41;](import-data-ssas-tabular.md)
- [Data Sources Supported &#40;SSAS Tabular&#41;](tabular-models/data-sources-supported-ssas-tabular.md)
- [Set the Data Type of a Column &#40;SSAS Tabular&#41;](tabular-models/set-the-data-type-of-a-column-ssas-tabular.md)
