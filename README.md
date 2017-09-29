PCS BingMap Key Manager Overview
================================
This is a tool to manage your BingMap key for your provisioned PCS

How to use the tool
==================
## Get the tool
There are two options to get this tool
1. You can download this tool from [Binary](https://github.com/IoTChinaTeam/PCSBingMapKeyManagerFX/tree/master/Binary) folder and save it to your local folder.
1. Clone this repo and open PCSBingMapKeyManagerFX.sln file with Visual Studio.net and build the solution. The output binaries will be under the bin folder.
## Configuration
* Find your Azure Cosmos DB url and key
	* Navigate the Cosmos DB in the Azure Portal (portal.azure.com > Resource Groups> Select Resource Group for your PCS > Select Azure Cosmos DB account)
	* Select Keys from the SETTINGS section and you can see URI and PRIMARY KEY in the page
* Modify PCSBingMapKeyManagerFX.exe.config file
	* Update the following lines with your own URI of Cosmos DB \
		`<setting name="Url" serializeAs="String">` \
    `<value>{url}</value>` \
    `</setting>`
  * Update the following lines with your own KEY of Cosmos DB \
		`<setting name="Key" serializeAs="String">` \
      `<value>{key}</value>` \
    `</setting>`
## Usage
* Run `PCSBingMapKeyManagerFX.exe` to view your current BingMap key of PCS
* Run `PCSBingMapKeyManagerFX.exe /newkey:{your new BingMap key}` to update a new key to your PCS
