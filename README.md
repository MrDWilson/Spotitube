Spotitube
=

Spotitube is a small group of microservices compiled
together from existing functionality. Its main purpose
was to solve a restriction in [this self-hosted discord
music bot.](https://github.com/jagrosh/MusicBot)

The bot wasn't able to process Spotify links, so I compiled
these services to take those URLs and convert them to YouTube
links for ease of use.

If you would simply like an API to convert Spotify to YouTube
links without the bot, this can be achieved by setting the
`RUN_BOT` environment variable to `False` (the default is
currently `True`).

Services
-

| Service name | Description |
| ----------- | ----------- |
| Manager | This .NET 6 service contains the API, as well as the URL parsing and playlist splitting functionality. |
| Token | This Python Flask API simply generates and caches a Spotify access token, to be used by the other services. |
| Songs | This JavaScript API will take a single Spotify track ID, and convert it to a YouTube ID using the [JavaScript Spotify to YouTube converter package](https://www.npmjs.com/package/spotify-to-youtube). |
| Bot | This service contains a JavaScript discord bot capable of catching Spotify music requests and injecting a YouTube conversion. |

Environment Variables
-

To set environment variables, it is recommended that you place a
`.env` file in the root directory with entries in the format
`ENV_VARIABLE_NAME=ENV_VARIABLE_VALUE`, seperated by new lines.

| Variable name | Description | Default |
| ----------- | ----------- | ----------- |
| SPOTIPY_CLIENT_ID | The client ID of your registered Spotify app. [More information here.](https://developer.spotify.com/documentation/general/guides/authorization/) | No default, required. |
| SPOTIPY_CLIENT_SECRET | The client secret of your registered Spotify app. [More information here.](https://developer.spotify.com/documentation/general/guides/authorization/) | No default, required. |
| SPOTIPY_REDIRECT_URI | The authorisation URL for Spotify. This can be left as default in most cases. | `http://127.0.0.1:8080` |
| SPOTIFY_CODE | The authorised user code from Spotify. The best way to get this is by running the Spotify auth in the [example here](https://developer.spotify.com/documentation/general/guides/authorization/), and then putting the authorised code into the file. **I am looking at improving this step.** | No default, required. |
| SPOTIFY_SCOPES | The scopes to authorise with the Spotify API. This must match whatever scopes you have defined on the Spotify Developer page when registering your app. If you want to access your private playlists, you will need to grant that. Otherwise, you can probably just use `playlist-read-collaborative`. | No default, required. |
| PORT | The port to run the API on. | 80 |
| EXTERNAL_NETWORK_NAME | The name of the Docker network to connect to for external web access. | No default, required. |
| MAX_TRACK_COUNT | The maximum amount of songs to convert from a playlist or album. This can be useful as large playlists may take a while, and you may also get blocked by the Spotify API. | 50 |
| TRACK_DELAY | The delay in milliseconds between Spotify API track requests. This should be left as default unless you are having issues with rate limiting. Try increasing it slightly if you do have issues. | 10 |
| USE_BOT | Whether to enable the included Discord bot. Setting this to false will turn this into just an API. | True |
| BOT_TOKEN | The authorised token for you registed Discord bot. | Optional, only needed if `USE_BOT` is set to `True` |
| BOT_COMMAND_PREFIXES | The prefixes to look for in Discord commands. | Optional, only needed if `USE_BOT` is set to `True`. Comma seperate values. |
| GENERATE_PLAYLISTS | Whether to wrap playlists in .txt files seperated by new lines. This can be useful as some Discord music bots can consume text playlist files. This will simplify album and playlist results by returning just the playlist name, rather than a set of links. | True |
| PLAYLIST_FOLDER | The folder on the host machine to map for playlist file saving. | Optional, only required if `GENERATE_PLAYLISTS` is set to `True`. |
| PLAYLIST_HISTORY | Whether to randomly generate the playlist names as GUIDs to allow history. If `False`, playlists will all be saved as `Spotify`, so will be overritten each time. | False |

Help
-

For any bugs/feature improvements, please use the issue system on this repository.

For help using the services, feel free to contact me on [danny@wlsn.xyz](danny@wlsn.xyz).
