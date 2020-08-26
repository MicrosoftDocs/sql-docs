---
title: Tutorial: Create a Jupyter Book extension
description: Learn about how to package a Jupyter Book into an extension using the extension generator
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: tutorial
author: anjalia
ms.author: t-lajian
ms.reviewer: alayu, maghan
ms.custom: 
ms.date: 08/26/2020
---

# Tutorial: Create a Jupyter Book extension

This tutorial demonstrates how to create a new Jupyter Book Azure Data Studio extension. The extension ships a sample Jupyter Book that can be opened and run in Azure Data Studio. 

During this tutorial you learn how to:
> [!div class="checklist"]
> * Create an extension project
> * Install the extension generator
> * Create your Jupyter Book extension
> * Run your extension
> * Package your extension
> * Publish your extension to the marketplace

### APIs used
- `bookTreeView.openBook`

### Extension use cases
There are a few different reasons why you would create a Jupyter Book extension: 
- Share organized and sectioned interactive documentation 
- Share a full book (akin to an e-book but distributed through Azure Data Studio)
- Version and keep track of Jupyter Book updates 

The main differentiation between a Jupyter Book and notebook extension is that a Jupyter Book provides you with organization. Tens of notebooks can be split into different chapters in a book, whereas the notebooks extension is intended to ship a small number of individual notebooks.

## Prerequisites

Azure Data Studio is built on the same framework as Visual Studio Code, so extensions for Azure Data Studio are built using Visual Studio Code. To get started, you need the following components:

- [Node.js](https://nodejs.org) installed and available in your `$PATH`. Node.js includes [npm](https://www.npmjs.com/), the Node.js Package Manager, which is used to install the extension generator.
- [Visual Studio Code](https://code.visualstudio.com) to make any changes to your extension and debug the extension.
- Ensure `azuredatastudio` is in your path. For Windows, make sure you choose the `Add to Path` option in setup.exe. For Mac or Linux, run the *Install 'azuredatastudio' command in PATH* option.

## Install the extension generator

To simplify the process of creating extensions, we've built an [extension generator](https://www.npmjs.com/package/generator-azuredatastudio) using Yeoman. To install it, run the following from the command prompt:

```console
`npm install -g yo generator-azuredatastudio`
```

## Create your extension

To create an extension:

1. Launch the extension generator with the following command:

   `yo azuredatastudio`

2. Choose **New Juptyter Book** from the list of extension types:

   ![extension generator](./media/tutorial-create-jupyterbook-extension/jupyterbook-extension-generator.png)

3. Follow the steps to fill in the extension name (for this tutorial, use **Test Book**), a publisher name (for this tutorial, use **Microsoft**), and add a description. 

At this point, you will select to either provide an existing Jupyter Book, use a provided sample book, or work to create a new Jupyter Book. All three options will be shown below.

### Providing an existing book

If you would like to ship a book that you have already created, provide the absolute file path to the folder where your book contents live. You will then be ready to move on to learning about the extension and shipping it.

![using an existing book](./media/tutorial-create-jupyterbook-extension/jupyterbook-existing-book.png)

### Using the sample book

If you do not have an existing book or notebooks, you can use the provided sample in the generator. 

![sample jupyter book](./media/tutorial-create-jupyterbook-extension/jupyterbook-sample-path.png)

The sample book demonstrates what a simple Jupyter Book looks like. If you would like to learn about customizing a Jupyter Book, see the following section on creating a new book with existing notebooks.

### Creating a new book

If you have notebooks that you would like to package into a Jupyter Book, you can! The generator will ask if you would like chapters in your book, and if so, how many and their titles. You can see what the selection process looks like below. Use the space bar to select which notebooks you want to place into each chapter. 

![create jupyter book](./media/tutorial-create-jupyterbook-extension/jupyterbook-create-book.png)

Completing the previous steps creates a new folder with your new Jupyter Book. Open the folder in Visual Studio Code and you're ready to ship your Jupyter Book extension!

## Understanding your extension 

This is what your project should currently look like:

   ![extension file structure](./media/tutorial-create-jupyterbook-extension/jupyterbook-filestructure-generator.png)

The `vsc-extension-quickstart.md` provides you with a reference of the important files. The `README.md` is where you can provide documentation for your new extension. Note the `package.json`, `jupyter-book.ts`, `content`, and `toc.yml` files. The `content` folder will hold all notebook or markdown files. The `toc.yml` will structure your Jupyter Book and is autogenerated if you opted to create a custom Jupyter Book through the Extension Generator. 

If you created a book using the generator and opted for chapters in your book, your folder structure would look a little different. Instead of your markdown and Jupyter Notebook files living in the `content` folder, there would be subfolders that correspond to the titles that you've chosen for your chapters. 

If there are any files or folders that you do not wish to publish, you can include their names in the `.vscodeignore` file.

Let's take a look at `jupyter-book.ts` to understand what our newly formed extension is doing. 

```javascript
// This function is called when you run the command `Launch Book: Test Book` from
// command palette in Azure Data Studio. If you would like any additional functionality
// to occur when you launch the book, add to the activate function.
export function activate(context: vscode.ExtensionContext) {
    context.subscriptions.push(vscode.commands.registerCommand('launchBook.test-book', () => {
        processNotebooks();
    }));

    // Add other code here if you want to register another command
}
```

The `activate` function is the main action of your extension. Any commands that you want to register should appear inside the `activate` function, similar to our `launchBook.test-book` command. Inside of the `processNotebooks` function, we find our extension folder that holds our Jupyter Book and call `bookTreeView.openBook` using our extension's folder as a parameter. 

The `package.json` file also plays an important role in registering our extension's command:

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

The activation event, `onCommand`, triggers the function that we registered when we invoke the command. There are a few other activation events that are possible for additional customization. For more information, see [Activation Events](https://code.visualstudio.com/api/references/activation-events).

## Package your extension

To share with others you need to package the extension into a single file. This can be published to the Azure Data Studio extension marketplace, or shared among your team or community. To do this, you need to install another npm package from the command line:

`npm install -g vsce`

Edit the `README.md` to your liking, then navigate to the base directory of the extension, and run `vsce package`. You can optionally link a repository with your extension or continue without one. To add one, add a similar line to your `package.json` file.

```json
"repository": {
    "type": "git",
    "url": "https://github.com/laurajjiang/testbook.git"
}
```

After these lines were added, my test-book-0.0.1.vsix file was created and ready to install in Azure Data Studio.

## Run your extension 

To run and test your extension, open Azure Data Studio and open the command palette (`Ctrl + Shift + P`). Find the command **Extensions: Install from VSIX** and navigate to the folder containing your new extension. It should now show up in your extension panel in Azure Data Studio.

   ![install-vsix](./media/tutorial-create-jupyterbook-extension/install-vsix.png)

Open the command palette again and find the command that we registered, **Launch Book: Test Notebook**. Upon running, it should open the Jupyter Book that we packaged with our extension.

   ![notebook-command](./media/tutorial-create-jupyterbook-extension/jupyterbook-launch-ads.png)

Congratulations! You built and can now ship your first Jupyter Book extension. For more information on Jupyter Books, see [Books with Jupyter](https://jupyterbook.org/intro.html).

## Publish your extension to the marketplace

The Azure Data Studio extension marketplace is not fully implemented yet. To publish, host the extension VSIX somewhere (for example, a GitHub Release page) and submit a PR updating [this JSON file](https://github.com/Microsoft/azuredatastudio/blob/release/extensions/extensionsGallery.json) with your extension info.

## Next steps

In this tutorial, you learned how to:
> [!div class="checklist"]
> * Create an extension project
> * Install the extension generator
> * Create your Jupyter Book extension
> * Package your extension
> * Publish your extension to the marketplace

We hope after reading this you'll have ideas on Jupyter Books that you'd like to share to the Azure Data Studio community. 

If you have an idea but are not sure how to get started, please open an issue or tweet at the team: [azuredatastudio](https://twitter.com/azuredatastudio).

You can always refer to the [Visual Studio Code extension guide](https://code.visualstudio.com/docs/extensions/overview) because it covers all the existing APIs and patterns.
