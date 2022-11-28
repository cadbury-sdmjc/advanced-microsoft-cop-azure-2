dotnet publish -o published && \
cd published && \
az webapp up --sku P1v2 --name advanced-microsoft-cop --os-type windows --runtime dotnet:6 && \
cd ..