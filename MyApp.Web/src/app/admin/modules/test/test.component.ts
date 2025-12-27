import { Component } from '@angular/core';

@Component({
  selector: 'app-test',
  template: `
    <div class="test-container">
      <h1>Test</h1>
      <p>Test sayfası - Bu sayfa test amaçlı oluşturulmuştur.</p>
    </div>
  `,
  styles: [`
    .test-container {
      padding: 2rem;
    }
    h1 {
      margin-bottom: 1rem;
    }
  `]
})
export class TestComponent {
}

