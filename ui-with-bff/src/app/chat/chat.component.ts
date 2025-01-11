import { Component, OnInit, signal } from '@angular/core';
import { ChatService } from './chat.service';
import { HttpDownloadProgressEvent, HttpEventType } from '@angular/common/http';
import { marked } from 'marked';
import { DomSanitizer } from '@angular/platform-browser';
import { PageTitleService } from '../core/page-title.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  templateUrl: './chat.component.html',
})
export class ChatComponent implements OnInit {
  streamedMessages = signal('');

  constructor(
    private pageTitleService : PageTitleService,
    private chatService: ChatService,
    private sanitizer: DomSanitizer,
  ) {}

  ngOnInit(): void {
    this.pageTitleService.setPageTitle({
      pageName: 'Home',
    });

    this.chatService.streamChat().subscribe((event) => {      
      if (event.type === HttpEventType.DownloadProgress) {
        // getting streamed content
        const partial = (event as HttpDownloadProgressEvent).partialText!;
        const responseObject = this.convertToObjectArray(partial!).join('');
        // The response in this example is a markdown string, so we need to convert it to HTML
        const updatedMessage = this.sanitizer.bypassSecurityTrustHtml(
          marked(responseObject) as string
        );
        this.streamedMessages.set(updatedMessage as string);
      }
      // Do stuff when the streaming is done (not needed in the case of this example)
      // else if (event.type === HttpEventType.Response) { }
    });
  }

  convertToObjectArray(responseContent: string) {
    if (responseContent.slice(-1) !== ']') {
      responseContent += ']';
    }
    return JSON.parse(responseContent);
  }
}