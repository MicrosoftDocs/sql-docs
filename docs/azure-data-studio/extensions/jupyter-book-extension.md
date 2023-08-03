---
title: Create a Jupyter Book extension
description: Learn how to package a Jupyter Book into an extension by using the extension generator.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 08/28/2020
ms.service: azure-data-studio
ms.topic: conceptual
---

# Create a Jupyter Book extension

This tutorial demonstrates how to create a new Jupyter Book Azure Data Studio extension. The extension ships a sample Jupyter Book that can be opened and run in Azure Data Studio.

In this article you learn how to:

> [!div class="checklist"]
> - Create an extension project.
> - Install the extension generator.
> - Create your Jupyter Book extension.
> - Run your extension.
> - Package your extension.
> - Publish your extension to the marketplace.

## APIs used

- `bookTreeView.openBook`

### Extension use cases

There are a few different reasons why you would create a Jupyter Book extension:

- Share organized and sectioned interactive documentation
- Share a full book (similar to an e-book but distributed through Azure Data Studio)
- Version and keep track of Jupyter Book updates

The main difference between a Jupyter Book and a notebook extension is that a Jupyter Book provides you with organization. Tens of notebooks can be split into different chapters in a Jupyter Book, but the notebook extension is intended to ship a small number of individual notebooks.

## Prerequisites

Azure Data Studio is built on the same framework as Visual Studio Code, so extensions for Azure Data Studio are built by using Visual Studio Code. To get started, you need the following components:

- [Node.js](https://nodejs.org) installed and available in your `$PATH`. Node.js includes [npm](https://www.npmjs.com/), the Node.js Package Manager, which is used to install the extension generator.
- [Visual Studio Code](https://code.visualstudio.com) to make any changes to your extension and debug the extension.
- Ensure `azuredatastudio` is in your path. For Windows, make sure you choose the **Add to Path** option in setup.exe. For Mac or Linux, run **Install 'azuredatastudio' command in PATH** from the Command Palette in Azure Data Studio.

## Install the extension generator

To simplify the process of creating extensions, we've built an [extension generator](https://www.npmjs.com/package/generator-azuredatastudio) using Yeoman. To install it, run the following command from the command prompt:

```console
npm install -g yo generator-azuredatastudio
```

## Create your extension

To create an extension:

1. Start the extension generator with the following command:

   `yo azuredatastudio`

1. Choose **New Jupyter Book** from the list of extension types.

   :::image type="content" source="media/jupyter-book-extension/jupyter-book-extension-generator.png" alt-text="Screenshot that shows the extension generator.":::

1. Follow the steps to fill in the extension name. For this tutorial, use **Test Book**. Then fill in a publisher name. For this tutorial, use **Microsoft**. Finally, add a description.

You select to either provide an existing Jupyter Book, use a provided sample book, or work to create a new Jupyter Book. All three options are shown.

### Provide an existing book

If you want to ship a book that you've already created, provide the absolute file path to the folder where your book contents live. You can then be ready to move on to learning about the extension and shipping it.

:::image type="content" source="media/jupyter-book-extension/jupyter-book-existing-book.png" alt-text="Screenshot that shows an existing book.":::

### Use the sample book

If you don't have an existing book or notebooks, you can use the provided sample in the generator.

:::image type="content" source="media/jupyter-book-extension/jupyter-book-sample-path.png" alt-text="Screenshot that shows a sample Jupyter book.":::

The sample book demonstrates what a simple Jupyter Book looks like. If you want to learn about customizing a Jupyter Book, see the following section on how to create a new book with existing notebooks.

### Create a new book

If you have notebooks that you want to package into a Jupyter Book, you can. The generator asks if you want chapters in your book, and if so, how many and their titles. You can see what the selection process looks like here. Use the Spacebar to select which notebooks you want to place in each chapter.

:::image type="content" source="media/jupyter-book-extension/jupyter-book-create-book.png" alt-text="Screenshot that shows creating Jupyter book.":::

Completing the previous steps creates a new folder with your new Jupyter Book. Open the folder in Visual Studio Code, and you're ready to ship your Jupyter Book extension.

## Understand your extension

This is what your project should currently look like:

   :::image type="content" source="media/jupyter-book-extension/jupyter-book-file-structure-generator.png" alt-text="Screenshot that shows an extension file structure.":::

The `vsc-extension-quickstart.md` file provides you with a reference of the important files. The `README.md` file is where you can provide documentation for your new extension. Note the `package.json`, `jupyter-book.ts`, `content`, and `toc.yml` files. The `content` folder holds all notebook or markdown files. The `toc.yml` structures your Jupyter Book and is autogenerated if you opted to create a custom Jupyter Book through the extension generator.

If you created a book by using the generator and opted for chapters in your book, your folder structure would look a little different. Instead of your markdown and Jupyter Notebook files living in the `content` folder, there would be subfolders that correspond to the titles that you chose for your chapters.

If there are any files or folders that you don't want to publish, you can include their names in the `.vscodeignore` file.

Let's take a look at `jupyter-book.ts` to understand what our newly formed extension is doing.

```javascript
// This function is called when you run the command `Launch Book: Test Book` from the
// command palette in Azure Data Studio. If you want any additional functionality
// to occur when you launch the book, add it to the activate function.
export function activate(context: vscode.ExtensionContext) {
    context.subscriptions.push(vscode.commands.registerCommand('launchBook.test-book', () => {
        processNotebooks();
    }));

    // Add other code here if you want to register another command.
}
```

The `activate` function is the main action of your extension. Any commands that you want to register should appear inside the `activate` function, similar to our `launchBook.test-book` command. Inside the `processNotebooks` function, we find our extension folder that holds our Jupyter Book and call `bookTreeView.openBook` by using our extension's folder as a parameter.

The `package.json` file also plays an important role in registering our extension's command.

```json
"activationEvents": [
		"onCommand:launchBook.test-book"
	],
	"main": "./out/notebook.js",
	"contributes": {
		"commands": [
			{
				"command": "launchBook.test-book",
				"title": "Launch Book: Test Book"
			}
		]
	}
```

The activation event, `onCommand`, triggers the function that we registered when we invoke the command. There are a few other activation events that are possible for additional customization. For more information, see [Activation events](https://code.visualstudio.com/api/references/activation-events).

## Package your extension

To share with others, you need to package the extension into a single file. Your extension can be published to the Azure Data Studio extension marketplace or shared with your team or community. To do this step, you need to install another npm package from the command line.

```console
npm install -g vsce
```

Edit the `README.md` file to your liking. Then go to the base directory of the extension, and run `vsce package`. You can optionally link a repository with your extension or continue without one. To add one, add a similar line to your `package.json` file.

```json
"repository": {
    "type": "git",
    "url": "https://github.com/laurajjiang/testbook.git"
}
```

After these lines are added, a `my test-book-0.0.1.vsix` file is created and ready to install in Azure Data Studio.

## Run your extension

To run and test your extension, open Azure Data Studio and open the command palette by selecting **Ctrl+Shift+P**. Find the command **Extensions: Install from VSIX**, and go to the folder that contains your new extension. It should now show up in your extension panel in Azure Data Studio.

   :::image type="content" source="media/jupyter-book-extension/install-vsix.png" alt-text="Screenshot that shows installing VSIX.":::

Open the command palette again, and find the command that we registered, **Launch Book: Test Notebook**. Upon running, it should open the Jupyter Book that we packaged with our extension.

   :::image type="content" source="media/jupyter-book-extension/jupyter-book-launch-ads.png" alt-text="Screenshot that shows the notebook-command.":::

Congratulations! You built and can now ship your first Jupyter Book extension. For more information on Jupyter Books, see [Books with Jupyter](https://jupyterbook.org/intro.html).

## Publish your extension to the Marketplace

The Azure Data Studio extension marketplace is under construction. To publish, host the extension VSIX somewhere, for example, on a GitHub release page. Submit a pull request that updates [this JSON file](https://github.com/Microsoft/azuredatastudio/blob/release/extensions/extensionsGallery.json) with your extension information.

## Next steps

In this tutorial, you learned how to:
> [!div class="checklist"]
> - Create an extension project.
> - Install the extension generator.
> - Create your Jupyter Book extension.
> - Package your extension.
> - Publish your extension to the marketplace.

We hope that after reading this article you'll have ideas on Jupyter Books that you'd like to share with the Azure Data Studio community.

If you have an idea but aren't sure how to get started, open an issue or tweet the team at [azuredatastudio](https://twitter.com/azuredatastudio).

For more information, the [Visual Studio Code extension guide](https://code.visualstudio.com/docs/extensions/overview) covers all the existing APIs and patterns.
