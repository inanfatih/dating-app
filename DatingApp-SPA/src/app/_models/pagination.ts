export interface Pagination {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

// Asagidaki class i hem users hem de messages icin kullanacagimiz icin Generic yaptik. Bu sayede hem message hem de user pass edebilecegiz
export class PaginatedResult<T>{
  result: T;
  pagination: Pagination;
}
