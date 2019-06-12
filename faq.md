---
title: FAQ
ptitle: Frequently Asked Questions
---
## What is Hidden Sounds?
Hidden Sounds is a group of independent artists joining together to support 100% independent releases.

The Spotify playlist is updated weekly with new music and there are streams on Twitch every Tuesday and Thursday at 2 PM EST.
Tuesday streams are more chill, playing the [Spotify playlist](https://open.spotify.com/user/ac7gpdbrhe2wjmigacy65mdls/playlist/2YDWLcBzTpDNQDfcQyy76b?si=DTkN1vapRIyjCesTGgA99g), while on Thursday member artists host the stream unless it's the monthly roundup. The monthly roundup features songs released in that month.

## Is it Thursday yet?
<span id="thursday">Maybe</span>
<script>
    const d = new Date().getDay();
    var text="";
    if(d == 1) text="No, but tomorrow's Tuesday!";
    else if(d == 2) text="No, but it's Tuesday!";
    else if(d == 3) text="Not yet, it's tomorrow!";
    else if(d == 4) text="Yes!";
    else text="No, but Tuesday is "+(d == 0 ? 9-d-7 : 9-d) //Hehe
    +" days away!";
    document.getElementById("thursday").innerText=text;
</script>
