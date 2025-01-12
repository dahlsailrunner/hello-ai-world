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
        const partialAsString = this.convertToString(partial!);
        // The response in this example is a markdown string, 
        // so we need to convert it to HTML
        const updatedMessage = this.sanitizer.bypassSecurityTrustHtml(
          marked(partialAsString) as string
        );
        this.streamedMessages.set(updatedMessage as string);
      }
      // Do stuff when the streaming is done (not needed in the case of this example)
      // else if (event.type === HttpEventType.Response) { }
    });
  }

  convertToString(responseContent: string): string {
    // the response might or might not be a valid JSON array, 
    // but it always starts with [, so we need to convert it to one
    if (responseContent.slice(-1) !== ']') {
      responseContent += ']';
    }
    return JSON.parse(responseContent).join('');
  }
}