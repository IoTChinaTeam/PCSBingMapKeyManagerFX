* Find your Cosmos DB url and key
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
