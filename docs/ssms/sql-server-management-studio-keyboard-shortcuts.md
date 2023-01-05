---
title: SQL Server Management Studio keyboard shortcuts
description: SQL Server Management Studio keyboard shortcuts
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - VS.ToolsOptionsPages.Environment.Keyboard
  - VS.ToolsOptionsPages.Environment.Keyboard.Query_Shortcuts
helpviewer_keywords: 
  - "SQL Server Management Studio [SQL Server], shortcuts"
  - "keyboard shortcuts [SQL Server Management Studio]"
  - "menu shortcuts [SQL Server Management Studio]"
  - "Query Editor [SQL Server Management Studio], keyboard shortcuts"
  - "hot keys [SQL Server Management Studio]"
  - "shortcuts [SQL Server Management Studio], keyboard shortcuts"
  - "keyboard shortcuts [SQL Server Management Studio], list of shortcuts"
  - "shortcuts [SQL Server Management Studio]"
  - "accelerator keys"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 02/08/2021
---

# SQL Server Management Studio keyboard shortcuts

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

SQL Server Management Studio (SSMS) offers keyboard shortcuts. By default, it uses the SQL Server scheme, with keyboard shortcuts based on Visual Studio. To change the keyboard scheme or add more keyboard shortcuts, on the **Tools** menu, select **Options**. Select the desired keyboard scheme on the **Environment**, **Keyboard** page.

> [!NOTE]
> To show only the headings, select **Collapse All** at the top of this page.

## Menu Activation keyboard shortcuts

| Action | Shortcut |
|--------|----------|
| Move to the SQL Server Management Studio menu bar | ALT |
| Activate the menu for a tool component|ALT+HYPHEN |
| Display the context menu|SHIFT+F10|
| Display the **New File** dialog box to create a file | CTRL+N |
| Display the **New Project** dialog box to create a new project|CTRL+SHIFT+N|
| Display the **Open File** dialog box to open an existing file|CTRL+O<br /><br /> or<br /><br /> CTRL+SHIFT+G|
| Display the **Open Project** dialog box to open an existing project|CTRL+SHIFT+O|
| Display the **Add New Item** dialog box to add a new file to the current project|CTRL+SHIFT+A|
| Display the **Add Existing Item** dialog box to add an existing file to the current project|SHIFT+ALT+A|
| Display the Query Designer|CTRL+SHIFT+Q|
| Close a menu or dialog box, canceling the action|ESC|

## Windows Management and Toolbar keyboard shortcuts  

| Action | Shortcut |
|--------|----------|
|Close the current MDI child window|CTRL+F4|
|Close a menu or dialog box, cancels an operation in progress, or focuses on the current document window|ESC|
|Print|CTRL+P|
|Exit|ALT+F4|
|Toggle full screen mode|SHIFT+ALT+ENTER|
|Close the current tool window|SHIFT+ESC|
|Cycle through the next MDI child windows|CTRL+F6|
|Display the IDE navigator with the first document window selected|CTRL+TAB|
|Cycle through the previous MDI child windows|CTRL+SHIFT+TAB|
|Moves the insertion point to the drop-down bar located at the top of the code editor when the editor is in Code view or Server Code view|CTRL+F2|
|Move to the current tool window toolbar|SHIFT+ALT|
|Display the IDE navigator with the first tool window selected|ALT+F7|
|Move to the next tool window|ALT+F6<br /><br /> or<br /><br /> F6 in the Database Engine Query Editor|
|Move to the previous tool window|SHIFT+ALT+F7|
|Move to the next pane of a split pane view of a single document|F6|
|Move to the previously selected window|SHIFT+ALT+F6<br /><br /> or<br /><br /> SHIFT+F6 in the  Database Engine  Query Editor|
|Move to the previous pane of a split pane view of a single document|SHIFT+F6|
|Display the dock menu|ALT+MINUS SIGN (-)|
|Display a popup listing all open windows|CTRL+ALT+DOWN ARROW|
|Opens a new query editor window|CTRL+O|
|Display Object Explorer|F8|
|Display Registered Servers|CTRL+ALT+G|
|Display Template Explorer|CTRL+ALT+T|
|Display Solution Explorer|CTRL+ALT+L|
|Display the Summary Window|F7|
|Display the Properties Window|F4|
|Display the **Output** window|CTRL+ALT+O|
|Display the **Task List** window|CTRL+\\, T<br /><br /> or<br /><br /> CTRL+\\, CTRL+T|
|Toggle between Object Explorer Details list view and Object Explorer Details property pane.|F6|
|Control the splitter bar that separates the Object Explorer Details list view and Object Explorer Details property pane to adjust the size of the display pane.|TAB, then UP arrow or DOWN arrow|
|Display the Toolbox|CTRL+ALT+X|
|Display the Bookmarks Window|CTRL+K, CTRL+W|
|Display the Browser Window|CTRL+ALT+R|
|Display a smart tag menu of common commands for Web server controls in the HTML designer|SHIFT+ALT+F10|
|Display the Error List Window ([!INCLUDE[tsql](../includes/tsql-md.md)] Editor only)|CRTL+\\, CTRL+E<br /><br /> or<br /><br /> CTRL+\\, E|
|Move to the next entry in the Error List window ([!INCLUDE[tsql](../includes/tsql-md.md)] Editor only)|CTRL+SHIFT+F12|
|Display the previous page in the viewing history. Available only in the Web browser window|ALT+LEFT ARROW|
|Display the next page in the viewing history. Available only in the Web browser window|ALT+RIGHT ARROW|

## Cursor Movement keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Move the cursor left|LEFT ARROW|
|Move the cursor right|RIGHT ARROW|
|Move up the cursor|UP ARROW|
|Move the cursor down|DOWN ARROW|
|Move the cursor to the beginning of the line|HOME|
|Move the cursor to the end of the line|END|
|Move the cursor to the beginning of the document|CTRL+HOME|
|Move the cursor to the end of the document|CTRL+END|
|Move up the cursor one screen|PAGE UP|
|Move the cursor down one screen|PAGE DOWN|
|Moves the cursor one word to the right|CTRL+ RIGHT ARROW|
|Moves the cursor one word to the left|CTRL+ LEFT ARROW|
|Returns the cursor to the last item.|SHIFT+F8|
|Moves the cursor to the top of the document|CTRL+PAGE UP|
|Moves to the previous tab in the document|CTRL+PAGE UP|
|Moves the cursor to the bottom of the document|CTRL+PAGE DOWN|
|Moves to the next tab in the document|CTRL+PAGE DOWN|

## Text Selection keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Select text from the cursor to the beginning of the document|CTRL+SHIFT+ HOME|
|Select text from the cursor to the end of the document|CTRL+SHIFT+END|
|Select text from the cursor to the start of the current line|SHIFT+HOME|
|Moves the cursor to the start of the current line and extends the column selection|SHIFT+ALT+HOME|
|Select text from the cursor to the end of the current line|SHIFT+END|
|Moves the cursor to the end of the line, extending the column selection.|SHIFT+ALT+END|
|Select text down line by line starting from the cursor|SHIFT+ DOWN ARROW|
|Moves the cursor down one line, extending the column selection|SHIFT+ Ctrl+Shift+Del|
|Moves the cursor one character to the left and extends the selection|SHIFT+LEFT ARROW|
|Moves the cursor one character to the left and extends the column selection|SHIFT+ALT+LEFT ARROW|
|Moves the cursor one character to the right and extends the selection|SHIFT+RIGHT ARROW|
|Moves the cursor one character to the right and extends the column selection|SHIFT+ALT+RIGHT ARROW|
|Select text up line by line starting from the cursor|SHIFT+UP ARROW|
|Move up the cursor one line, extending the selection|SHIFT+ALT+ UP ARROW|
|Extend selection up one page|SHIFT+ PAGE UP|
|Extend selection down one page|SHIFT+ PAGE DOWN|
|Select the entire current document|CTRL+A|
|Select the word containing the cursor, or the closest word|CTRL+W|
|Select the current location in the editor, back to the previous location in the editor|CTRL+=|
|Extend the selection to the top of the current window|CTRL+SHIFT+ PAGE UP|
|Move the cursor to the last line in view, extending the selection|CTRL+SHIFT+ PAGE DOWN|
|Extend the selection one word to the right|CTRL+SHIFT+ RIGHT ARROW| 
|Extend the selection one word to the left|CTRL+SHIFT+ LEFT ARROW|
|Move the cursor to the right one word, extending the selection|CTRL+SHIFT+ALT+ RIGHT ARROW|
|Move the cursor to the left one word, extending the selection|CTRL+SHIFT+ALT+ LEFT ARROW|
|Move the cursor to the next brace, extending the selection|CTRL+SHIFT+]|
|Select the text from the current location of the cursor to the Navigate Backward (CTRL+MINUS SIGN (-)) location|CTRL+EQUAL SIGN (=)|
|Go back to the previous document or window in the navigation history|CTRL+MINUS SIGN (-)|
|Go forward to the next document or window in the navigation history|CTRL+SHIFT+MINUS SIGN (-)|
|Swaps the anchor and end points of the current selection|CTRL+K, CTRL+A|
|Moves the cursor to the first line in view, extending the selection|CTRL+SHIFT+PAGE UP|
|Moves the cursor to the last line in view, extending the selection|CTRL+SHIFT+PAGE DOWN|

## Bookmark keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Set or remove a bookmark at the current line|CTRL+K, CTRL+K|
|Next bookmark|CTRL+K, CTRL+N|
|If the current bookmark is in a folder, moves to the next bookmark in the folder. Bookmarks outside the folder are skipped.<br /><br /> If the bookmark isn't in a folder, moves to the next bookmark at the same level.<br /><br /> If the Bookmarks window contains folder, bookmarks in folders are skipped.|CTRL+SHIFT+K, CTRL+SHIFT+N|
|Previous bookmark|CTRL+K, CTRL+P|
|If the current bookmark is in a folder, moves to the next bookmark in the folder. Bookmarks outside the folder are skipped.<br /><br /> If the bookmark isn't in a folder, moves to the next bookmark at the same level.<br /><br /> If the Bookmarks window contains folder, bookmarks in folders are skipped.|CTRL+SHIFT+K, CTRL+SHIFT+P|
|Clear bookmarks|CTRL+K, CTRL+L|

## Tree Control keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Collapse tree nodes|- (on the numeric keypad)|
|Expand all tree nodes|* (on the numeric keypad)|
|Scroll up the tree control in the window|CTRL+UP ARROW|
|Scroll the tree control down in the window|CTRL+DOWN ARROW|

## Code Editor keyboard shortcuts

All shortcuts aren't implemented in all types of code editors.
 
| Action | Shortcut |
|--------|----------|
|Toggle the full-screen display|SHIFT+ALT+ENTER|
|Scroll text up one line|CTRL+UP ARROW|
|Scroll text down one line|CTRL+DOWN ARROW|
|Reverse the last editing action|CTRL+Z<br /><br /> or<br /><br /> ALT+BACKSPACE| 
|Restore the previously undone edit|CTRL+SHIFT+Z<br /><br /> or<br /><br /> CTRL+Y<br /><br /> or<br /><br /> ALT+SHIFT+BACKSPACE|
|Save the selected item|CTRL+S|
|Save all|CTRL+SHIFT+S|
|Close|CTRL+F4|
|Print|CTRL+P|
|Exit|ALT+F4|
|Open the current file in a browser|CTRL+SHIFT+W|
|Delete all text in the current file|CTRL+SHIFT+DEL|
|Display the **Go To Line** dialog box|CTRL+G|
|Display the **Navigate To** dialog box.|CTRL+PLUS SIGN (+)|
|Increase line indent|TAB|
|Decrease line indent|SHIFT+TAB|
|Make the selected text upper case|CTRL+SHIFT+U|
|Make the selected text lower case|CTRL+U|
|Make the selected text a comment|CTRL+K, CTRL+C|
|Uncomment the selected text|CTRL+K, CTRL + U|
|Open a new query with current connection|CTRL+N|
|Open database in Object Explorer|ALT+F8|
|Specify values for template parameters|CTRL+SHIFT+M|
|Run the selected portion of the query editor or the entire query editor if nothing is selected|F5<br /><br /> or<br /><br /> CTRL+SHIFT+E|
|Parse the selected portion of the query editor or the entire query editor if nothing is selected|CTRL+F5|
|Display the estimated execution plan|CTRL+SHIFT+ALT+L|
|Cancel the executing query|ALT+BREAK|
|Include actual execution plan in the query output|CTRL+SHIFT+ALT+M|
|Output results in a grid|CTRL+SHIFT+D|
|Output results in text format|CTRL+T|
|Output results to a file|CTRL+SHIFT+T|
|Show or hide the query results pane|CTRL+R|
|Show the query results pane|CTRL+SHIFT+ALT+R|
|Toggle between query and results pane|F6|
|Copy the result grid and headers to the clipboard|CTRL+SHIFT+C|
|Move to the next active window in SSMS|ALT+F6|
|Open [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)]|CTRL+ALT+P|
|Display the Query Designer dialog from the query editor window|CTRL+SHIFT+Q|
|Run the **sp_help** system stored procedure|ALT+F1|
|Run the **sp_who** system stored procedure|CTRL+1|
|Run the **sp_lock** system stored procedure|CTRL+2|
|Run the stored procedure configured for this shortcut in the **Tools**, **Options**, **Keyboard**, **Query Shortcuts** dialog|CTRL+3|
|Run the stored procedure configured for this shortcut in the **Tools**, **Options**, **Keyboard**, **Query Shortcuts** dialog|CTRL+4|
|Run the stored procedure configured for this shortcut in the **Tools**, **Options**, **Keyboard**, **Query Shortcuts** dialog|CTRL+5|
|Run the stored procedure configured for this shortcut in the **Tools**, **Options**, **Keyboard**, **Query Shortcuts** dialog|CTRL+6|
|Run the stored procedure configured for this shortcut in the **Tools**, **Options**, **Keyboard**, **Query Shortcuts** dialog|CTRL+7|
|Run the stored procedure configured for this shortcut in the **Tools**, **Options**, **Keyboard**, **Query Shortcuts** dialog|CTRL+7|
|Run the stored procedure configured for this shortcut in the **Tools**, **Options**, **Keyboard**, **Query Shortcuts** dialog|CTRL+8|
|Run the stored procedure configured for this shortcut in the **Tools**, **Options**, **Keyboard**, **Query Shortcuts** dialog|CTRL+9|
|Run the stored procedure configured for this shortcut in the **Tools**, **Options**, **Keyboard**, **Query Shortcuts** dialog|CTRL+0|

## Text Manipulation in Code Editor keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Insert a new line|ENTER or SHIFT+ENTER|
|Swap the characters on either side of the cursor (doesn't apply to the SQL Editor.)|CTRL+T|
|Delete one character to the right of the cursor|DELETE|
|Delete one character to the left of the cursor|BACKSPACE<br /><br /> or<br /><br /> SHIFT+<br /><br /> BACKSPACE|
|Delete whitespace in the selection, or deletes whitespace next to the cursor if there is no selection|CTRL+K, C|
|Insert the number of spaces configured for the editor|TAB|
|Insert a blank line above the cursor|CTRL+ENTER|
|Insert a blank line below the cursor|CTRL+SHIFT+ ENTER|
|Change the selected text to lowercase|CTRL+SHIFT+L|
|Change the selected text to uppercase|CTRL+SHIFT+U|
|Toggle between insert mode and overtype mode|INSERT|
|Move selected lines to the left on tab stop|SHIFT+TAB|
|Delete the word to the right of the cursor|CTRL+DELETE|
|Delete the word to the left of the cursor|CTRL+BACKSPACE|
|Transpose the words on either side of the cursor (doesn't apply to the SQL Editor.)|
|Moves the line containing the cursor below the next line|SHIFT+ALT+T|
|Applies the indenting and space formatting for the language specified on the **Formatting** pane of the language in the **Text Editor** section of the **Options** dialog. Available only in the text editor.|CTRL+K, CTRL+D|
|Correctly indents the selected lines of code based on the surrounding lines of code|CTRL+K, CTRL+F|
|Set or remove a shortcut in the current line|CTRL+K, CTRL+H|
|Remove the comment syntax from the current line|CTRL+K, CTRL+U|
|Shows or hides spaces and tabs|CTRL+R, CTRL+W|
|Enables or disables word wrap in an editor|Alt+F, CTRL+W|
|Collapses all outlining regions to show just the outermost groups in the hierarchy|CTRL+M, CTRL+A|
|Collapses the currently selected outlining region|CTRL+M, CTRL+S|
|Expands all outlining regions on the page|CTRL+M, CTRL+X|
|Expands the currently selected outlining region|CTRL+M, CTRL+E|
|Collapses existing outlining regions|CTRL+M, CTRL+O|
|Hides the selected text. A signal icon marks the location of the hidden text|CTRL+M, CTRL+H|
|Toggles all text sections previously marked as hidden between the hidden and display states.|CTRL+M, CTRL+L|
|Toggles the currently selected hidden text section between the hidden and display states|CTRL+M, CTRL+M|
|Removes all outlining information in the document|CTRL+M, CTRL+P|
|Removes the outlining information for the currently selected region|CTRL+M, CTRL+U|

## Microsoft IntelliSense keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|List members|CTRL+J|
|Complete word|CTRL+SPACE<br /><br /> or<br /><br /> ALT+RIGHT ARROW|
|Display quick information|CTRL+K, CTRL+I|
|Display parameter information|CTRL+SHIFT+SPACE|
|Copy parameter tip|CTRL+SHIFT+ALT+C|
|Paste parameter tip|CTRL+SHIFT+ALT+P|
|Jump between syntax pairs|CTRL+]|
|Launch code snippet picker|CTRL+K, CTRL+X|
|Refresh local cache|CTRL+SHIFT+R|
|Launch Surround With snippet picker|CTRL+K, CTRL+S|
|Display the Code Snippet Manager|CTRL+K, CTRL+B|
|Changes the IntelliSense filter level from the **Common** tab to the **All** tab.|ALT+PLUS SIGN (+)|
|Changes the IntelliSense filter level from the **All** tab to the **Common** tab.|ALT+PERIOD (.)|

## Document Window and Browser keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Toggle full-screen mode|SHIFT+ALT+ENTER|
|Move to the next pane of a split pane view of a document|F6|
|Move to the previous document in the editor or designer|CTRL+SHIFT+F6<br /><br /> CTRL+SHIFT+TAB|
|Move to the previous pane of a document in split pane view|SHIFT+F6|
|Back, display the previous page in the viewing history|ALT+LEFTARROW|
|Forward, display the next page in the viewing history|ALT+RIGHTARROW|
|Closes a menu or dialog box, cancels an operation in progress, or places focus in the current window|ESC|

## Solution Explorer keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Display Solution Explorer|CTRL+ALT+L|
|Display the **New File** dialog box to create a new file|CTRL+N|
|Display the **New Project** dialog box to create a new project|CTRL+SHIFT+N|
|Display the **Open File** dialog box to open an existing file|CTRL+O|
|Change the name of the selected object|F2|

## Help and Books Online keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Help|F1<br /><br /> or<br /><br /> SHIFT+F1|
|Display SQL Server Books Online|CTRL+F1|
|Open the Help Library Manager|CTRL+ALT+F1|
|Display the SQL Server Resource Center Web page|CTRL+ALT+F2|
|Display help for the current editor window|SHIFT+F1|

## Search keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Display the **Find** dialog box|CTRL+F|
|Displays the definition for the selected symbol.|F12|
|Displays the list of references for the selected symbol.|SHIFT+F12|
|Display the **Replace** dialog box|CTRL+H|
|Start incremental search. Type the characters to search for or press CTRL+I to search for characters from the previous search|CTRL+I|
|Find the next occurrence of the previous search text|F3|
|Find the previous occurrence of the search text|SHIFT+F3|
|Find the next occurrence of the currently selected text|CTRL+F3|
|Find the previous occurrence of the currently selected text|CTRL+SHIFT+F3|
|Display the **Replace in Files** dialog box|CTRL+SHIFT+H|
|Reverse incremental search so it starts at the bottom of the file and searches to the top|CTRL+SHIFT+I|
|Select or clear the **Search up** option in **Find and Replace**|ALT+F3, B|
|Stop the **Find in Files** search|ALT+F3, S|
|Select or clear the **Find whole word** option in **Find and Replace**|ALT+F3, W|
|Selects or clears the **Wildcard** option in **Find and Replace**|ALT+F3, P|
|Place the caret in the Find/Command box of the Standard toolbar|CTRL+/|

## Cut and Paste keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Cut (delete the currently selected item and place it to the Clipboard)|CTRL+X|
|Cuts all of the selected lines, or the current line if nothing is selected.|CTRL+L<br /><br /> or<br /><br /> CTRL+SHIFT+L|
|Copy to the Clipboard|CTRL+C<br /><br /> or<br /><br /> CTRL+INSERT|
|Paste from the Clipboard at the insertion point|CTRL+V<br /><br /> or<br /><br /> SHIFT+INSERT|
|Pastes an item from the Clipboard Ring at the insertion point and automatically selects the pasted item|CTRL+SHIFT+V|

## Activity Monitor keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Launches Activity Monitor|CTRL+ALT+A|
|Closes Activity Monitor|CTRL+F4|
|Refresh|F5|
|Filter the monitor display|CTRL+SHIFT+F|
|Cycle through panels|F6|
|Expand or collapse selected pane|CTRL and + or -|
|Expand or collapse all panes|+ or -|
|Copies entire selected row in grid|CTRL+C|
|Copy cell|CTRL+SHIFT+C|
|Drop down for filtering in grid|ALT+DOWN|
|Scroll up or down Activity Monitor|CTRL+ALT+UP/DOWN|

## Replication Monitor keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Refresh|F5|
|Open a detail window from a grid|ENTER|

## Replication Conflict Viewer keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Define filter|F6|
|Apply filter|F7|
|Show all columns|F8|

## Query Designer keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Cancels or stops the currently running query|CTRL+T|
|Displays the diagram pane of the **Query Designer**|CTRL+1|
|Displays the criteria pane of the **Query Designer**|CTRL+2|
|Displays the SQL pane of the **Query Designer**|CTRL+3|
|Displays the results pane of the **Query Designer**|CTRL+4|
|Run the query specified in the **Query Designer**|CTRL+R|
|When in the results pane, moves focus to the tool strip docked at the bottom of the designer|CTRL+G|
|Enables JOIN mode in the **Query Designer**|CTRL+SHIFT+J|

## Designer keyboard shortcuts

| Action | Shortcut |
|--------|----------|
|Move the selected control down in increments of 8 on the design surface|DOWN ARROW|
|Move the selected control left in increments of 8 on the design surface|LEFT ARROW|
|Move the selected control right in increments of 8 on the design surface|RIGHT ARROW|
|Move up the selected control in increments of 8 on the design surface|UP ARROW|
|Increases the height of the selected control in increments of 8|SHIFT+DOWN ARROW|
|Reduces the width of the selected control in increments of 8|SHIFT+LEFT ARROW|
|Increases the width of the selected control in increments of 8|SHIFT+RIGHT ARROW|
|Decreases the height of the selected control in increments of 8|SHIFT+UP ARROW|
|Moves to the next control on the page|TAB|
|Moves to the previous control on the page|SHIFT+TAB|
|Display the grid on the design surface|ENTER|

## See Also

- [Customize Menus and Shortcut Keys](customize-menus-and-shortcut-keys.md)