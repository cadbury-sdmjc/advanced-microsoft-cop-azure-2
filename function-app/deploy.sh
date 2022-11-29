DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true dotnet publish -o published && \
cd published && \
zip -r app.zip . && \
az functionapp deployment source config-zip -g microsft-cop -n advanced-microsoft-cop-func --src app.zip && \
cd ..