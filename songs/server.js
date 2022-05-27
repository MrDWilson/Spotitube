'use strict';

const express = require('express');
const SpotifyToYoutube = require('spotify-to-youtube');
const SpotifyWebApi = require('spotify-web-api-node');
const axios = require('axios');
// Constants
const PORT = 80;
const HOST = '0.0.0.0';

const spotifyApi = new SpotifyWebApi();

const spotifyToYoutube = SpotifyToYoutube(spotifyApi);

async function getSong(song) {
  return await spotifyToYoutube(song);
}

// App
const app = express();
app.use(express.json());

app.post('/', function(request, response) {
  const getUrl = async (url) => await (await axios.get(url));
  let _ = getUrl('http://spotitube_token/').then(resp => {
      spotifyApi.setAccessToken(resp.data);
      const _ = getSong(request.body.url).then(song => {
        response.send(song);
      });
    });
});

app.listen(PORT, HOST);
console.log(`Running on http://${HOST}:${PORT}`);
