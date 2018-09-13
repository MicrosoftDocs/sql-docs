---
title: Extend the functionality of Azure Data Studio (preview) | Microsoft Docs
description: Learn about extending Azure Data Studio (preview)
ms.custom: "tools|sos"
ms.date: "09/24/2018"
ms.reviewer: "alayu; sstein"
ms.prod: sql
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Getting started with [!INCLUDE[name-sos](../includes/name-sos-short.md)] extensibility

[!INCLUDE[name-sos](../includes/name-sos.md)] has several extensibility mechanisms to customize the user experience and make those customizations available to the entire user community.  The core [!INCLUDE[name-sos](../includes/name-sos.md)] platform is built upon Visual Studio Code, so most of the Visual Studio Code extensibility APIs are available.  Additionally, we've provided additional extensibility points for data management specific activities.

Some of the key extensibility points are listed below.

* Visual Studio Code extensibility APIs
* SQL Operations Studio extension authoring tools
* Manage Dashboard tab panel contributions
* Insights with Actions experiences
* SQL Operations Studio extensibility APIs
* Custom Data Provider APIs

## Visual Studio Code extensibility APIs
Please see take a look at [Extension Authoring](https://code.visualstudio.com/docs/extensions/overview) and [Extension API](https://code.visualstudio.com/docs/extensionAPI/overview) documentation on the VS Code website.

## Manage Dashboard tab panel contributions
Please see [Contribution Points](#contribution-points) and [Context Variables](#context-variables).

## Azure Data Studio extensibility APIs
Please see [Extensibility APIs](#extensibility-apis).


## Contribution points

This section covers the various contribution points that are defined in the package.json extension manifest.

The intellisense is supported inside sqlopsstudio.

## Contributes dashboard

Contribute tab, container, insight widget to the dashboard.

![Dashboard](media/extensibility/dashboard-page.png)

`dashboard.tabs`

Dashboard.tabs creates the tab sections inside the dashboard page. It expects an object or an array of objects.  

```
"dashboard.tabs": [
	{
		"id": "test-tab1",
		"title": "Test 1",
		"description": "The test 1 displays a list of widgets.",
		"when": "connectionProvider == 'MSSQL' && !mssql:iscloud",
		"alwaysShow": true,
		"container": {
			…
		}
	}
]
```

`dashboard.containers`

Instead of specifying dashboard container inline (inside the dashboard.tab). You can register containers using dashboard.containers. It accepts an object or an array of the object.

```
"dashboard.containers": [
	{
		"id": "innerTab1",
		"container": {
			…
		}
	},
	{
		"id": "innerTab2",
		"container": {
			…
		}
	}
]
```

To refer to registered container, you can simply specify the id of the container

```
"dashboard.tabs": [
	{
		...
		"container": {
			"innerTab1": {             
			}
		}
	}
]

```

`dashboard.insights`

You can register insights using dashboard.insights. This is similiar to [Tutorial: Build a custom insight widget](https://docs.microsoft.com/en-us/sql/sql-operations-studio/tutorial-build-custom-insight-sql-server)

```
"dashboard.insights": {
	"id": "my-widget"
	"type": {
		"count": {
			"dataDirection": "vertical",
			"dataType": "number",
			"legendPosition": "none",
            "labelFirstColumn": false,
			"columnsAsLabels": false
        }
    },
    "queryFile": "{your file folder}/activeSession.sql"
}
```


### Dashboard container types

There are 4 different container types that we currently support:

1. `widgets-container`

    ![Widgets container](media/extensibility/widgets-container.png)


	The list of widgets that will be displayed in the container. It’s a flow layout. It accepts the list of widgets.

	```
	"container": {
		"widgets-container": [
			{
				"widget": {
					"query-data-store-db-insight": {
					}
				}
			},
			{
				"widget": {
					"explorer-widget": {
					}
				}
			}
		]
	}
	```
	
2. 	`webview-container`

    ![webview container](media/extensibility/webview-container.png)

	The webview will be displayed in the entire container. It expects webview id to be the same is tab ID

	```
	"container": {
		"webview-container": null
	}
	```
	
3. 	`grid-container`

    ![grid container](media/extensibility/grid-container.png)

	The list of widgets or webviews that will be displayed in the grid layout

	```
	"container": {
		"grid-container": [
			{
				"name": "widget 1",
				"widget": {
					"explorer-widget": {
					}
				},
				"row":0,
				"col":0
			},
			{
				"name": "widget 2",
				"widget": {
					"tasks-widget": {
						"backup", 
						"restore",
						"configureDashboard",
						"newQuery"
					}
				},
				"row":0,
				"col":1
			},
			{
				"name": "Webview 1",
				"webview": {
					"id": "google"
				},
				"row":1,
				"col":0,
				"colspan":2
			},
			{
				"name": "widget 3",
				"widget": {
					"explorer-widget": {}
				},
				"row":0,
				"col":3,
				"rowspan":2
			},
		]
	```

4. 	`nav-section`

    ![nav section](media/extensibility/nav-section.png)

	The navigation section will be displayed in the container

	```
	"container": {
		"nav-section": [
			{
				"id": "innerTab1",
				"title": "inner-tab1",
				"icon": {
					"light": "./icons/tab1Icon.svg",
					"dark": "./icons/tab1Icon_dark.svg"
				}
				"container": {
					…
				}
			},
			{
				"id": "innerTab2",
				"title": "inner-tab2",
				"icon": {
					"light": "./icons/tab2Icon.svg",
					"dark": "./icons/tab2Icon_dark.svg"
				}
				"container": {
					…
				}
			}
		]
	}
	```



## Context variables

For general information about context in vscode and subsequently Azure Data Studio, see [Extensibility](https://code.visualstudio.com/docs/extensionAPI/extension-points#_example).

In Azure Data Studio we have specific context around database connections available for extensions.

### Dashboard

In dashboard we provide the following context variables.

`connectionProvider` - A string of the identifier for the provider of the current connection. Ex. `connectionProvider == 'MSSQL'`.

`serverName` - A string of the server name of the current connection. Ex. `serverName == 'localhost'`.

`databaseName` - A string of the database name of the current connection. Ex. `databaseName == 'master'`.

`connection` - The full connection profile object for the current connection (IConnectionProfile)

`dashboardContext` - A string of the context of the page of the dashboard is currently on. Either 'database' or 'server'. Ex. `dashboardContext == 'database'`




## Extensibility APIs

SQL Operations Studio provides an API that extensions can use to interact with other parts of the application such as Object Explorer. These APIs are available from the [`src/sql/sqlops.d.ts`](https://github.com/Microsoft/sqlopsstudio/blob/master/src/sql/sqlops.d.ts) file and are described below.

## Connection Management
`sqlops.connection`

_Available starting in version 0.26.7 (February Public Preview)_

### Top-level Functions

- `getCurrentConnection(): Thenable<sqlops.connection.Connection>`
Gets the current connection based on the active editor or Object Explorer selection.

- `getActiveConnections(): Thenable<sqlops.connection.Connection[]>`
Gets a list of all the user's connections that are active. Returns an empty list if there are no such connections.

- `getCredentials(connectionId: string): Thenable<{ [name: string]: string }>`
Gets a dictionary containing the credentials associated with a connection. These would otherwise be returned as part of the options dictionary under a `sqlops.connection.Connection` object but get stripped from that object. 

### `Connection`
- `options: { [name: string]: string }` The dictionary of connection options
- `providerName: string` The name of the connection provider (e.g. "MSSQL")
- `connectionId: string` The unique identifier for the connection

### Example Code
```
> let connection = sqlops.connection.getCurrentConnection();
connection: {
	providerName: ‘MSSQL’,
	connectionId: ‘d97bb63a-466e-4ef0-ab6f-00cd44721dcc’,
	options: {
		server: ‘mairvine-sql-server’,
		user: ‘sa’,
		authenticationType: ‘sqlLogin’,
		…
	},
	…
}
> let credentials = sqlops.connection.getCredentials(connection.connectionId);
credentials: {
	password: ‘abc123’
}

```

## Object Explorer
`sqlops.objectexplorer`

_Available starting in version 0.27.3 (March Public Preview)_

### Top-level Functions
- `getNode(connectionId: string, nodePath?: string): Thenable<sqlops.objectexplorer.ObjectExplorerNode>`
Get an Object Explorer node corresponding to the given connection and path. If no path is given, it returns the top-level node for the given connection. If there is no node at the given path, it returns `undefined`. Note: The `nodePath` for an object is generated by the SQL Tools Service backend and is difficult to construct by hand. Future API improvements will allow you to get nodes based on metadata you provide about the node, such as name, type and schema.

- `getActiveConnectionNodes(): Thenable<sqlops.objectexplorer.ObjectExplorerNode>`
Get all active Object Explorer connection nodes.

- `findNodes(connectionId: string, type: string, schema: string, name: string, database: string, parentObjectNames: string[]): Thenable<sqlops.objectexplorer.ObjectExplorerNode[]>`
Find all Object Explorer nodes that match the given metadata. The `schema`, `database`, and `parentObjectNames` arguments should be `undefined` when they are not applicable. `parentObjectNames` is a list of non-database parent objects, from highest to lowest level in Object Explorer, that the desired object is under. For example, when searching for a column "column1" that belongs to a table "schema1.table1" and database "database1" with connection ID `connectionId`, call `findNodes(connectionId, 'Column', undefined, 'column1', 'database1', ['schema1.table1'])`. Also see the [list of types that SQL Operations Studio supports by default](https://github.com/Microsoft/sqlopsstudio/wiki/Object-Explorer-types-supported-by-FindNodes-API) for this API call.

### ObjectExplorerNode
- `connectionId: string`
The id of the connection that the node exists under

- `nodePath: string`
The path of the node, as used for a call to the `getNode` function.

- `nodeType: string`
A string representing the type of the node

- `nodeSubType: string`
A string representing the subtype of the node

- `nodeStatus: string`
A string representing the status of the node

- `label: string`
The label for the node as it appears in Object Explorer

- `isLeaf: boolean`
Whether the node is a leaf node and therefore has no children

- `metadata: sqlops.ObjectMetadata`
Metadata describing the object represented by this node

- `errorMessage: string`
Message shown if the node is in an error state

- `isExpanded(): Thenable<boolean>`
Whether the node is currently expanded in Object Explorer

- `setExpandedState(expandedState: vscode.TreeItemCollapsibleState): Thenable<void>`
Set whether the node is expanded or collapsed. If the state is set to None, the node will not be changed.

- `setSelected(selected: boolean, clearOtherSelections?: boolean): Thenable<void>`
Set whether the node is selected. If `clearOtherSelections` is true, clear any other selections when making the new selection. If it is false, leave any existing selections. `clearOtherSelections` defaults to true when `selected` is true and false when `selected` is false.

- `getChildren(): Thenable<sqlops.objectexplorer.ObjectExplorerNode[]>`
Get all the child nodes of this node. Returns an empty list if there are no children.

- `getParent(): Thenable<sqlops.objectexplorer.ObjectExplorerNode>`
Get the parent node of this node. Returns undefined if there is no parent.

### Example Code
```
private async interactWithOENode(selectedNode: sqlops.objectexplorer.ObjectExplorerNode): Promise<void> {
	let choices = ['Expand', 'Collapse', 'Select', 'Select (multi)', 'Deselect', 'Deselect (multi)'];
	if (selectedNode.isLeaf) {
		choices[0] += ' (is leaf)';
		choices[1] += ' (is leaf)';
	} else {
		let expanded = await selectedNode.isExpanded();
		if (expanded) {
			choices[0] += ' (is expanded)';
		} else {
			choices[1] += ' (is collapsed)';
		}
	}
	let parent = await selectedNode.getParent();
	if (parent) {
		choices.push('Get Parent');
	}
	let children = await selectedNode.getChildren();
	children.forEach(child => choices.push(child.label));
	let choice = await vscode.window.showQuickPick(choices);
	let nextNode: sqlops.objectexplorer.ObjectExplorerNode = undefined;
	if (choice === choices[0]) {
		selectedNode.setExpandedState(vscode.TreeItemCollapsibleState.Expanded);
	} else if (choice === choices[1]) {
		selectedNode.setExpandedState(vscode.TreeItemCollapsibleState.Collapsed);
	} else if (choice === choices[2]) {
		selectedNode.setSelected(true);
	} else if (choice === choices[3]) {
		selectedNode.setSelected(true, false);
	} else if (choice === choices[4]) {
		selectedNode.setSelected(false);
	} else if (choice === choices[5]) {
		selectedNode.setSelected(false, true);
	} else if (choice === 'Get Parent') {
		nextNode = parent;
	} else {
		let childNode = children.find(child => child.label === choice);
		nextNode = childNode;
	}
	if (nextNode) {
		let updatedNode = await sqlops.objectexplorer.getNode(nextNode.connectionId, nextNode.nodePath);
		this.interactWithOENode(updatedNode);
	}
}

vscode.commands.registerCommand('mssql.objectexplorer.interact', () => {
	sqlops.objectexplorer.getActiveConnectionNodes().then(activeConnections => {
		vscode.window.showQuickPick(activeConnections.map(connection => connection.label + ' ' + connection.connectionId)).then(selection => {
			let selectedNode = activeConnections.find(connection => connection.label + ' ' + connection.connectionId === selection);
			this.interactWithOENode(selectedNode);
		});
	});
});
```

## Proposed APIs

We have added proposed APIs to allow extensions to display custom UI in dialogs, wizards, and document tabs, among other capabilities. See the [proposed API types file](https://github.com/Microsoft/sqlopsstudio/blob/master/src/sql/sqlops.proposed.d.ts) for more documentation, though be aware that these APIs are subject to change at any time. Examples of how to use some of these APIs can be found in the ["sqlservices" sample extension](https://github.com/Microsoft/sqlopsstudio/tree/master/samples/sqlservices).


