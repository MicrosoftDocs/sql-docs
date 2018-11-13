---
title: "Step 1: Configure development environment for Ruby development | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 8cdbadeb-f640-406c-977c-d2d44b7b5368
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step 1: Configure development environment for Ruby development
You will need to configure your development environment with the prerequisites in order to develop an application using the Ruby Driver for SQL Server.    
  
Note that the Ruby Driver uses the TDS protocol, which is enabled by default in SQL Server and Azure SQL Database.  No additional configuration is required.  
  
  
## Windows  
  
1.  **Download Ruby Installer**  
If your machine does not have Ruby please install it. For new ruby users, we recommend you use Ruby 2.2.X installers. These provide a stable language and a extensive list of packages (gems) that are compatible and updated. Go the [Ruby download page](https://rubyinstaller.org/downloads/) and download the appropriate 2.1.x installer. For example if you are on a 64 bit machine, download the Ruby 2.1.6 (x64) installer.   
  
2.  **Install Ruby**  
Once the installer is downloaded, do the following:  
a. Double-click the file to start the installer.  
b. Select your language, and agree to the terms.  
c.  On the install settings screen, select the check boxes next to both Add Ruby executables to your PATH and Associate .rb and .rbw files with this Ruby installation.  
  
3.  **Download Ruby DevKit**  
Download DevKit from the RubyInstaller page  
  
4.  **Install Ruby DevKit**  
After the download is finished, do the following:  
a. Double-click the file. You will be asked where to extract the files.  
b. Click the "..." button, and select "C:\DevKit". You will probably need to create this folder first by clicking "Make New Folder".  
c. Click "OK", and then "Extract", to extract the files.  
  
5. **Open cmd.exe**  
  
6. **Initialize Ruby DevKit**  
```  
> chdir C:\DevKit  
> ruby dk.rb init  
> ruby dk.rb install  
```  
  
7.  **Install TinyTDS gem**  
```  
> gem inst tiny_tds
```  
  
## Ubuntu Linux  
  
1. **Open terminal**  
  
2. **Install Ruby Version Manager (rvm) and prerequisites**  
```  
> sudo apt-get --assume-yes update  
> command curl -sSL https://rvm.io/mpapis.asc | gpg --import -  
> curl -L https://get.rvm.io | bash -s stable  
> source ~/.rvm/scripts/rvm  
```  
   
3. **Use rvm to install Ruby**  
For example, install version 2.3.0 of Ruby:  
```  
> rvm install 2.3.0  
> rvm use 2.3.0 --default  
> ruby -v  
```  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ensure that the output of the last command indicates you are running version 2.3.0.  
  
4.  **Install FreeTDS**  
```  
> sudo apt-get --assume-yes install freetds-dev freetds-bin  
```  
  
5.  **Install TinyTDS**  
```  
> gem install tiny_tds  
```  
  
## Mac  
  
Note that Mac OS X already has Ruby pre-installed, as the OS has a dependency.    
  
1.  **Open terminal**  
  
2. **Install Homebrew package manager**  
```  
> ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"  
```  
  
3.  **Install FreeTDS**  
```  
> brew install FreeTDS  
```  
  
4.  **Install TinyTDS gem**  
```  
> gem install tiny_tds  
```
