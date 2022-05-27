'use strict';

const express = require('express');
const SpotifyToYoutube = require('spotify-to-youtube');
const SpotifyWebApi = require('spotify-web-api-node');
const axios = require('axios');
// Constants
const PORT = 80;
const HOST = '0.0.0.0';

const yaml = require('js-yaml');
const fs   = require('fs');
const config = yaml.load(fs.readFileSync('config.yaml', 'utf8'));

const spotifyApi = new SpotifyWebApi();

const spotifyToYoutube = SpotifyToYoutube(spotifyApi);

async function getSong(song) {
  return await spotifyToYoutube(song);
}

// App
const app = express();
app.use(express.json());

app.post('/', function(request, response) {
  const token = async (url) => await (await axios.get(url));
  let _ = token('http://spotitube_token/').then(resp => {
      spotifyApi.setAccessToken(resp.data);
      const _ = getSong(request.body.url).then(song => {
	      console.log(song);
        response.send(song);
      });
    });
});

app.listen(PORT, HOST);
console.log(`Running on http://${HOST}:${PORT}`);
