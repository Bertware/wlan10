# wlan10
Wi-fi network manager for windows 10. Bringing back the functionality Microsoft removed, and more.

If I helped you, please say thanks! (how you can do this is below)

## Requirements

* Windows 10 (Might also work for windows 7,8)
* .NET 4 or higher
* Wireless networks

## Usage

Drag and drop your networks to your liking, save, enjoy.  
Setting autoconnect and autoswitching(read more about autoswitching below) doesn't require saving.  
The tool is only required for setup, and can be removed once everything is configured.

See also: http://blog.bertware.net/2016/07/set-windows-network-priority-wlan10/

> **Note:** What is autoswitching?

> The autoSwitch (WLANProfile) element determines the roaming behavior of an auto-connected network when a more preferred network is in range. . This element is optional.

> If autoSwitch is "true" and connectionMode is set to "auto", a connection to a more preferred network must be attempted whenever a more preferred network comes into range. A more preferred network is one that is ordered higher in the list of preferred wireless networks. This connection attempt occurs when connected to another network.

> An autoSwitch value set to "true" results in a higher frequency of periodic scanning for new networks. This may result in increased radio frequency pollution from these periodic scans and increased power consumption used by the wireless network adapter.

## Support
Create a git issue if you have suggestions or problems

## Say thanks

Say thanks! Star this project so I can see the impact, [buy me some pizza](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=K4856LBVQZ25L), or leave me a message!

## Credits
### Project
Idea conceived by Bertware, coded by Bertware.
www.bertware.net

This project is subject to the Mozilla Public License Version 2.0.  
A copy of the license is included in this repository.

### Libraries used in this project
#### Gong-wpf-dragrop

The gong wpf dragrop library is used as an NuGet package, and is included in the exe in executably form.
https://github.com/punker76/gong-wpf-dragdrop

Gong-wpf-dragdrop code is public under the BSD 3-Clause License
    
    Copyright (c) 2015, Jan Karger (Steven Kirk)
    All rights reserved.
    
    Redistribution and use in source and binary forms, with or without
    modification, are permitted provided that the following conditions are met:
    
    Redistributions of source code must retain the above copyright notice, this
    list of conditions and the following disclaimer.
    
    Redistributions in binary form must reproduce the above copyright notice,
    this list of conditions and the following disclaimer in the documentation
    and/or other materials provided with the distribution.
    
    Neither the name of gong-wpf-dragdrop nor the names of its
    contributors may be used to endorse or promote products derived from
    this software without specific prior written permission.
    
    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
    AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
    IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
    FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
    SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
    CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
    OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
    OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
