# wlan10

Wi-fi network manager for Windows 10. Bringing back the functionality Microsoft removed, and more.

If I helped you, please say thanks! (how you can do this is below)

## Requirements

* Windows 10 or 8.1 (may also work on 8, 7)
* .NET 4 or higher
* Wireless networks

## Usage

Open the app. Some wifi networks should show up in the list.

> **If no wifi networks show up, you need to connect to some wireless networks first!**  
> Setting up a wireless network through the >app is not supported.

Drag and drop the networks to change the order to your liking. Hit the save button to store this new order. Windows should now prefer the networks which you put on top, selecting the highest positioned available network. Setting autoconnect and autoswitching (read more about autoswitching below) doesn't require saving.  

The tool is only required for setup, and can be removed once everything is configured. See [the blog for more](http://blog.bertware.net/2016/07/set-windows-network-priority-wlan10/).

> **Note:** What is autoswitching?
>
> The `autoSwitch` (`WLANProfile`) element determines the roaming behavior of an auto-connected network when a more preferred network is in range. This element is optional.
>
> If `autoSwitch` is `true` and `connectionMode` is set to `auto`, a connection to a more preferred network must be attempted whenever a more preferred network comes into range. A more preferred network is one that is ordered higher in the list of preferred wireless networks. This connection attempt occurs when connected to another network.
>
> An `autoSwitch` value set to `true` results in a higher frequency of periodic scanning for new networks. This may result in increased radio frequency pollution from these periodic scans and increased power consumption used by the wireless network adapter.

## Support

Create a [GitHub issue](https://github.com/Bertware/wlan10/issues) if you have suggestions or problems.

## Say thanks

Say thanks! Star this project so I can see the impact, [buy me some pizza](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=K4856LBVQZ25L), or leave me a message!

## Credits

Idea conceived by Bertware, coded by [Bertware](http://www.bertware.net).

This project is subject to the Mozilla Public License Version 2.0. A copy of the license is included in this repository.

### Libraries used in this project
#### GongSolutions.WPF.DragDrop

The [`GongSolutions.WPF.DragDrop`](https://github.com/punker76/gong-wpf-dragdrop) library is used as an NuGet package, and is included in the exe in executable form.

`GongSolutions.WPF.DragDrop` code is public under the BSD 3-Clause License.

    Copyright © 2015, Jan Karger (Steven Kirk). All rights reserved.

    Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

    Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation  and/or other materials provided with the distribution.

    Neither the name of gong-wpf-dragdrop nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
