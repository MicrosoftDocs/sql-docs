---
title: "Find Databases Wizard (AccessToSQL)"
description: Learn about the Find Databases Wizard in SQL Server Migration Assistant for Access, including Select Files, Select Locations, and Verify Selection.
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.access.selectdatabase.f1"
  - "ssma.access.finddatabase.f1"
  - "ssma.access.verifydatabase.f1"
helpviewer_keywords:
  - "Find Databases Wizard"
---
# Find Databases Wizard (AccessToSQL)

There are three pages in the Find Databases Wizard.

- [Select Files](#select-files-page)
- [Select Locations](#select-locations-page)
- [Verify Selection](#verify-selection-page)

## Select Files page

The Select Files page of the Find Databases Wizard lists the files that were found during the search. To select a file to add to SSMA, select the row that contains the file name. When you're finished selecting files, select **Next** to view the Verify Selection page.

To open the Scan Network for Files Wizard, on the **File** menu, select **Find Databases**.

To select a file to add to SSMA, select the check box next to the file name. When you're finished selecting files, select **Next** to view the Completion page.

### Options

#### Select all

Select this check box to select all databases in the list.

#### Unselect all

Select this check box to deselect all databases in the list.

#### File name

Shows the name of the Access database file to add to SSMA.

#### Owner

Displays the owner of the file.

#### File path

Displays the path of the Access database file.

#### Size

The size of the database, in bytes.

#### Created

The date and time the database was created.

#### Modified

The date and time the database was last updated.

## Select Locations page

On the Select Locations page of the Find Databases Wizard, you enter the search parameters for finding Access databases.

To open the Find Databases Wizard, on the **File** menu, select **Find Databases**. For more information about how to use this dialog box, see [Add and remove Access database files](adding-and-removing-access-database-files-accesstosql.md)

### Options

#### Browse

Select to browse the computer or network. Select the folder or location to search, and then select **OK**. Select **Add** to add this location to the list.

#### Path box

Shows the path to add to the locations box. Enter or browse to the path you want to scan, and then select the **Add** button.

#### Locations box

Lists the locations SSMA scans. When you select the **Add** button, the specified path is added to this box.

#### Add

Adds the location in the path text box to the list of locations.

#### Replace

If an item in the list of locations is selected, the item in the path text box replaces the selected item.

#### Remove

Removes the selected item from the list of locations to search.

#### All or part of file name

To find databases that have names that contain a specific string, enter the string. Don't use wildcard characters.

#### Creation date

To find databases that were created within a specific date range, enter the start date in the **From** box and the end date in the **To** box.

#### Last updated date

To find databases that were last updated within a specific date range, enter the start date in the **From** box and the end date in the **To** box.

#### Size

To find databases that match a size criteria, select a comparison character in the first box, enter a number for the size, and then select bytes, kilobytes, or megabytes from the third box.

#### Owner

Enter the full Windows user name of the owner. This might be formatted as *domain*\\*user*.

## Verify Selection page

The Verify Selection page of the Find Databases Wizard lists the files to be added to SSMA. If the list of files is incorrect, select **Back** to modify the list. Otherwise, select **Finish** to add the files.

To open the Find Databases wizard, on the **File** menu, select **Find Databases**.

### Options

#### File name

Shows the name of the Access database file to add to SSMA.

#### File path

Shows the path of the Access database file.

#### Owner

Shows the owner of the file.

## Related content

- [Add and remove Access database files](adding-and-removing-access-database-files-accesstosql.md)
- [User interface reference](user-interface-reference-accesstosql.md)
