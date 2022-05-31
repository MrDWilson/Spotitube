import os
from xmlrpc.client import DateTime
from flask import Flask
import spotipy
from datetime import datetime, timedelta
from typing import Optional

app = Flask(__name__)

code = os.environ['SPOTIFY_CODE']

scopes = os.environ['SPOTIFY_SCOPES']
if not scopes:
    raise Exception('SPOTIFY_SCOPES environment variable is required.')

auth_manager = spotipy.oauth2.SpotifyOAuth(scope=scopes,
                                           show_dialog=True)

token: Optional[str] = None
expiry: Optional[DateTime] = None

@app.route('/')
def request_token():
    global token
    global expiry
    if token is not None and expiry is not None and expiry > datetime.now():
        return token

    token_info = auth_manager.get_access_token(code)
    token = token_info['access_token']
    expiry = datetime.now() + timedelta(seconds=int(token_info['expires_in']))
    return token

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=80)
