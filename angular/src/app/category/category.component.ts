import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { CategoryDto, CategoryService } from '../proxy/categories';

@Component({
  selector: 'app-category',
  standalone: false,
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss',
  providers: [ListService], // Liệt kê ListService để sử dụng trong component
})
export class CategoryComponent implements OnInit {

  categories: PagedResultDto<CategoryDto> = {
    items: [],
    totalCount: 0
  }; // Biến để lưu danh sách danh mục

  constructor(public readonly list: ListService,
    private categoryService: CategoryService) {
    // Khởi tạo ListService để sử dụng trong component

  }

  ngOnInit() {
    // Logic khởi tạo khi component được khởi tạo
    // Ví dụ: gọi API để lấy danh sách danh mục
    const categoryStreamCreator = (query) => this.categoryService.getList(query);

    this.list.hookToQuery(categoryStreamCreator).subscribe((response) => {
      this.categories = response; // Lưu kết quả trả về vào biến categories
    });

  }

}
