# Space Library Search 🚀

This repository contains the source code used to create the [Space Library](https://nasa.curiosity.ai/) AI-powered search demo using [Curiosity](https://curiosity.ai). The data used for this demo includes papers and reports published by NASA on their [public library](https://sti.nasa.gov/). 

This website uses our AI-enabled graph and search technology to enable you to explore the dataset using machine-learning-based synonyms and find similar papers using word and graph embeddings.

Check more details on our accompanying [blog post](https://medium.com/curiosity-ai/space-library-f30f9a526d26).

## Running Curiosity Locally

[Check our documentation](https://docs.curiosity.ai/en/articles/4449019-installation) to install a free instance of Curiosity on your computer or clould environment of preference. For this demo, you'll need approximately 100GB of disk space for the dataset download and further ingestion on your locally running Curiosity instance.

Once you have your Curiosity instance up and running, check the [initial setup guide](https://docs.curiosity.ai/en/articles/4452603-initial-setup) and then you'll be ready to ingest the NASA papers using the code in this repository.

## Data Ingestion

The code in this repository will download a subset of the NASA library between 2010 and 2020 to your computer, and then ingest the data to your Curiosity instance.

You'll need to generate an API token for your system, and change the code to use this token. Check the [documentation on how to create an API token](https://docs.curiosity.ai/en/articles/4453131-external-data-connectors).

```bash
git clone https://github.com/curiosity-ai/space-library
cd space-library
dotnet run {SERVER_URL} {AUTH_TOKEN}
```

You need to replace `{SERVER_URL}` with the address your server is listing to (usually `http://localhost:8080` if you're running it locally), and `{AUTH_TOKEN}` with the API token generated earlier.

## Acknowledgements

The dataset used for this demo is a subset of the publicly available data from the [NASA STI library](https://sti.nasa.gov/harvesting-data-from-ntrs/): https://sti.nasa.gov/harvesting-data-from-ntrs/). For more information, check the [NASA STI Library Terms and Conditions of Use](https://sti.nasa.gov/disclaimers/).
